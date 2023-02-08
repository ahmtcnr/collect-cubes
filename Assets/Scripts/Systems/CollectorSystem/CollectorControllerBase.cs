using System;
using UnityEngine;


public abstract class CollectorControllerBase : MonoBehaviour
{
    public abstract event Action<Vector3> OnPointerDrag;

    protected virtual void OnEnable()
    {
        Actions.OnGameStateChanged += HandleGameState;
    }

    protected virtual void OnDisable()
    {
        Actions.OnGameStateChanged -= HandleGameState;
    }

    protected abstract void HandleGameState(GameState state);
}