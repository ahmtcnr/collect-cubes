using System.Collections;
using UnityEngine;

public class LevelManagerFountain : LevelManagerBase
{
    [SerializeField] private FountainSettings _fountainSettings;

    private Coroutine _fountainRoutine;

    protected override void HandleGameState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GenerateLevel:
                StartFountain();
                StartCounter(_fountainSettings.CountdownTime);
                Actions.OnLevelGenerated?.Invoke();
                break;
            case GameState.StartGame:
                _isLevelStarted = true;
                break;
            case GameState.EndGame:
                _isLevelStarted = false;
                StopFountain();
                break;
        }
    }

    private void StartFountain()
    {
        _fountainRoutine = StartCoroutine(_fountainSettings._fountainPattern.CO_Behaviour(_fountainSettings));
    }

    private void StopFountain()
    {
        if (_fountainRoutine != null)
        {
            StopCoroutine(_fountainRoutine);
        }
    }
}