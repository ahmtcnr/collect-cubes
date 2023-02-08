using System;
using UnityEngine;

public abstract class CollectableCounterBase : ScriptableObject, ISerializationCallbackReceiver
{
    [NonSerialized] private int _totalCollectableCount;
    [NonSerialized] private int _currentCollectedAmount;

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
        if (state == GameState.EndGame)
        {
            ResetData();
        }
    }

    private void ResetData() => _totalCollectableCount = CurrentCollectedAmount = 0;
    public event Action OnCollected;
    public event Action OnClearCollectables;
    public int TotalCollectableCount
    {
        get => _totalCollectableCount;
        set
        {
            _totalCollectableCount = value;
            
            
            if (_totalCollectableCount == 0)
            {
                OnClearCollectables?.Invoke();
            }
        }
    }

    public int CurrentCollectedAmount
    {
        get => _currentCollectedAmount;
        set
        {
            _currentCollectedAmount = value;
            OnCollected?.Invoke();
        }
    }
    public void OnAfterDeserialize() => ResetData();

    public void OnBeforeSerialize()
    {
    }
}