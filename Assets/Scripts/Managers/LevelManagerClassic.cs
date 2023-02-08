using UnityEngine;

public class LevelManagerClassic : LevelManagerBase
{
    [SerializeField] private PrinterData _printerData;

    [SerializeField] private PlayerCollectableCounter _playerCollectableCounter;

    protected override void OnEnable()
    {
        base.OnEnable();
        _playerCollectableCounter.OnClearCollectables += RaiseWinEvent;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _playerCollectableCounter.OnClearCollectables -= RaiseWinEvent;
    }

    private void RaiseWinEvent() => Actions.OnWin?.Invoke();

    protected override void HandleGameState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GenerateLevel:
                PrintCubes();
                Actions.OnLevelGenerated?.Invoke();
                break;
        }
    }

    private void PrintCubes()
    {
        var levelToPrint = PreferencesManager.Instance.GetCurrentLevelIndex() % _printerData.ImageSettings.Length;

        var imageSettings = _printerData.ImageSettings[levelToPrint];
        var texture2D = imageSettings.Texture2D;

        var offset = new Vector3(imageSettings.Texture2D.width, 0, imageSettings.Texture2D.height) / 2;

        for (int x = 0; x < texture2D.width; x++)
        {
            for (int y = 0; y < texture2D.height; y++)
            {
                var candidatePixelColor = texture2D.GetPixel(x, y);
                if (candidatePixelColor.a == 0)
                {
                    continue;
                }

                var worldPos = (new Vector3(x, 0, y) - offset) * imageSettings.CubeScale;
                worldPos.y = imageSettings.CubeScale / 2;

                _ = SetCollectableProperties(worldPos, imageSettings.CubeScale, candidatePixelColor);
                _playerCollectableCounter.TotalCollectableCount++;
            }
        }
    }
}