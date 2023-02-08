using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState _gameState;

    private GameState GameState
    {
        get => _gameState;
        set
        {
            _gameState = value;
            Actions.OnGameStateChanged?.Invoke(_gameState);
        }
    }

    private void Awake()
    {
#if !UNITY_EDITOR
        Application.targetFrameRate = 60;
#endif
    }

    private void Start()
    {
        UpdateGameState(GameState.GenerateLevel);
    }

    private void OnEnable()
    {
        Actions.OnLevelGenerated += HandleLevelGeneration;
        Actions.OnPointerDown += HandlePointerDown;

        Actions.OnWin += HandleWin;
        Actions.OnLose += HandleLose;
    }

    private void OnDisable()
    {
        Actions.OnLevelGenerated -= HandleLevelGeneration;
        Actions.OnPointerDown -= HandlePointerDown;

        Actions.OnWin -= HandleWin;
        Actions.OnLose -= HandleLose;
    }

    private void HandleLose()
    {
        UpdateGameState(GameState.LoseGame);
        UpdateGameState(GameState.EndGame);
        Invoke(nameof(Start), 1);
    }

    private void HandleWin()
    {
        UpdateGameState(GameState.WinGame);
        UpdateGameState(GameState.EndGame);
        Invoke(nameof(Start), 1);
    }

    private void HandlePointerDown()
    {
        if (_gameState == GameState.ReadyToStart)
        {
            UpdateGameState(GameState.StartGame);
        }
    }

    private void HandleLevelGeneration() => UpdateGameState(GameState.ReadyToStart);
    private void UpdateGameState(GameState gameState) => GameState = gameState;
}

public enum GameState
{
    GenerateLevel,
    ReadyToStart,
    StartGame,
    WinGame,
    LoseGame,
    EndGame,
}