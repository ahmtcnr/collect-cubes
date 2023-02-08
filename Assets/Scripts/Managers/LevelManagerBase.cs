using System.Collections;
using UnityEngine;

public abstract class LevelManagerBase : MonoBehaviour
{
    [SerializeField] protected TimerData _timerData;
    protected bool _isLevelStarted;

    protected virtual void OnEnable()
    {
        Actions.OnGameStateChanged += HandleGameState;
    }

    protected virtual void OnDisable()
    {
        Actions.OnGameStateChanged -= HandleGameState;
    }

    protected abstract void HandleGameState(GameState gameState);


    protected void StartCounter(float countdown)
    {
        StartCoroutine(TimerTick());

        IEnumerator TimerTick()
        {
            _timerData.CountdownTime = countdown;
            yield return new WaitUntil(() => _isLevelStarted);
            while (!_timerData.IsTimeOut)
            {
                _timerData.TimerTick();
                yield return new WaitForSeconds(_timerData.TimeScale);
            }

            CheckLevelCondition();
        }
    }

    protected virtual void CheckLevelCondition()
    {
        Actions.OnWin?.Invoke();
    }

    public static GameObject SetCollectableProperties(Vector3 worldPosition, float scale, Color color)
    {
        var cube = ObjectPool.Instance.GetObject();

        cube.transform.position = worldPosition;

        if (cube.TryGetComponent(out IShapeHandler shapeHandler))
        {
            shapeHandler.ChangeMaterialColor(color);
            shapeHandler.SetScale(Vector3.one * scale);
        }

        return cube;
    }
}