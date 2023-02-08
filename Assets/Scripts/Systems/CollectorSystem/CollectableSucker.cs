using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CollectableSucker : MonoBehaviour
{
    [SerializeField] private CollectableCounterBase _collectableCounter;

    [SerializeField] private CollectorSystemSettings _collectorSystemSettings;

    [SerializeField] private CollectableSuckerSettings _collectableSuckerSettings;

    [SerializeField] private Renderer _mesh;

    private void Awake()
    {
        SetPosition();
        _mesh.material.SetColor("_BaseColor", _collectableSuckerSettings.CollectColor);
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
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectable collectable) && !collectable.IsCollected())
        {
            collectable.Collect(_collectableSuckerSettings, transform.position);
            _collectableCounter.CurrentCollectedAmount++;
            _collectableCounter.TotalCollectableCount--;
        }
    }

    private void SetPosition()
    {
        transform.position = _collectorSystemSettings.SuckerPosition;
    }
}