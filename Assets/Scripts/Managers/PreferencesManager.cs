using UnityEngine;

public class PreferencesManager : Singleton<PreferencesManager>
{
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
            IncreaseLevelIndex();
        }
    }

    private void IncreaseLevelIndex()
    {
        var currentLevel = PlayerPrefs.GetInt(CURRENT_LEVEL_INDEX, 0);

        currentLevel++;

        PlayerPrefs.SetInt(CURRENT_LEVEL_INDEX, currentLevel);
    }

    public int GetCurrentLevelIndex()
    {
        return PlayerPrefs.GetInt(CURRENT_LEVEL_INDEX, 0);
    }

    private const string CURRENT_LEVEL_INDEX = "CurrentLevelIndex";
}