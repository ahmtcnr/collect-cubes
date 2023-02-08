using System;
public static class Actions
{
    public static Action<GameState> OnGameStateChanged;

    public static Action OnLevelGenerated;
    public static Action OnPointerDown;

    public static Action OnWin;

    public static Action OnLose;
}