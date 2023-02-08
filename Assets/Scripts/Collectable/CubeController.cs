using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(IShapeHandler))]
public class CubeController : MonoBehaviour, ICollectable, IPushable
{
    private int _initialLayer;

    private IShapeHandler _shapeHandler;
    private Rigidbody _rb;

    private bool _isCollected;

    private Coroutine _moveSequence;

    private void Awake()
    {
        _initialLayer = gameObject.layer;
        _rb = GetComponent<Rigidbody>();
        _shapeHandler = GetComponent<IShapeHandler>();
    }

    private void OnEnable()
    {
        Actions.OnGameStateChanged += HandleGameState;
    }

    private void OnDisable()
    {
        Actions.OnGameStateChanged -= HandleGameState;
    }

    private void HandleGameState(GameState state)
    {
        switch (state)
        {
            case GameState.EndGame:
                ReturnToPool();
                break;
            case GameState.StartGame:
                ActivateCollectable();
                break;
        }
    }

    private void ActivateCollectable()
    {
        _rb.isKinematic = false;
    }

    private void ReturnToPool()
    {
        _rb.isKinematic = true;
        transform.rotation = Quaternion.identity;
        gameObject.layer = _initialLayer;
        _isCollected = false;
        ObjectPool.Instance.ReturnToPool(gameObject);
    }
    public bool IsCollected() => _isCollected;
    public void Collect(CollectableSuckerSettings collectableSuckerSettings, Vector3 collectPosition)
    {
        _isCollected = true;
        gameObject.layer = collectableSuckerSettings.LayerAfterCollection.GetLayer();

        _shapeHandler.ChangeMaterialColor(collectableSuckerSettings.CollectColor);

        StartCoroutine(CO_AddForceToDirection(collectableSuckerSettings, collectPosition));
    }
    private IEnumerator CO_AddForceToDirection(CollectableSuckerSettings collectableSuckerSettings, Vector3 target)
    {
        var elapsedTime = 0f;
        while (elapsedTime < collectableSuckerSettings.DelayTimeToInactivation)
        {
            var targetDir = (target - transform.position).normalized;
            _rb.AddForce(targetDir * collectableSuckerSettings.CollectionForce);
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        _rb.isKinematic = true;
    }
    public void Push(Vector3 force)
    {
        _rb.isKinematic = false;
        _rb.AddForce(force, ForceMode.Impulse);
    }
}