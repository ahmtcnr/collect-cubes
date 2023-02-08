using TMPro;
using UnityEngine;

public class CounterUIHandler : MonoBehaviour
{
    [SerializeField] private CollectableCounterBase _collectableCounterBase;
    [SerializeField] private TMP_Text _counterText;

    [SerializeField] private CollectorSystemSettings _collectorSystemSettings;


    private void Awake()
    {
        SetInitialColor();
    }


    private void OnEnable()
    {
        _collectableCounterBase.OnCollected += UpdateCollectText;
    }

    private void OnDisable()
    {
        _collectableCounterBase.OnCollected -= UpdateCollectText;
    }

    private void UpdateCollectText()
    {
        _counterText.text = _collectableCounterBase.CurrentCollectedAmount.ToString();
    }

    private void SetInitialColor()
    {
        _counterText.color = _collectorSystemSettings.CollectorColor;
    }
}