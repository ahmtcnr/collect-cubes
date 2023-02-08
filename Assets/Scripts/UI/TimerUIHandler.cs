using TMPro;
using UnityEngine;

public class TimerUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

    [SerializeField] private TimerData _timerData;


    private void OnEnable()
    {
        _timerData.OnTimerTicks += UpdateTimer;
    }


    private void OnDisable()
    {
        _timerData.OnTimerTicks -= UpdateTimer;
    }

    private void UpdateTimer()
    {
        _timerText.text = _timerData.CountdownTime.ConvertToTimer();
    }
}