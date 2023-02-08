using UnityEngine;

public class LevelManagerContest : LevelManagerFountain
{
    [SerializeField] private PlayerCollectableCounter _playerCollectableCounter;
    [SerializeField] private EnemyCollectableCounter _enemyCollectableCounter;

    protected override void CheckLevelCondition()
    {
        if (_playerCollectableCounter.CurrentCollectedAmount >= _enemyCollectableCounter.CurrentCollectedAmount)
        {
            Actions.OnWin?.Invoke();
        }
        else
        {
            Actions.OnLose?.Invoke();
        }
    }
}