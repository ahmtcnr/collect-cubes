using System;
using UnityEngine;


[CreateAssetMenu(menuName = "TimerData", order = 0)]
public class TimerData : ScriptableObject
{
    [NonSerialized] private float _countdownTime;
    [SerializeField] private float _timeScale = 1;


    public event Action OnTimeOut;
    public event Action OnTimerTicks;

    public float TimeScale => _timeScale;

    public bool IsTimeOut => CountdownTime <= 0;

    public float CountdownTime
    {
        get => _countdownTime;
        set
        {
            _countdownTime = value;
            OnTimerTicks?.Invoke();
        }
    }

    public void TimerTick()
    {
        if (IsTimeOut)
            return;

        CountdownTime -= 1;
        if (IsTimeOut)
            OnTimeOut?.Invoke();
    }
}