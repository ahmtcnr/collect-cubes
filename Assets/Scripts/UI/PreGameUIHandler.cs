using System.Collections;
using TMPro;
using UnityEngine;

public class PreGameUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _dragText;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _blinkInterval;

    private Coroutine _blinkSequence;

    private void OnEnable()
    {
        StartBlinkingSequence();
    }

    private void StartBlinkingSequence()
    {
        if (_blinkSequence != null)
        {
            StopCoroutine(_blinkSequence);
        }

        _blinkSequence = StartCoroutine(StartBlinking());

        IEnumerator StartBlinking()
        {
            var startColor = _dragText.color;

            var elapsedTime = 0f;

            while (true)
            {
                startColor.a = _animationCurve.Evaluate(elapsedTime / _blinkInterval);
                _dragText.color = startColor;

                if (elapsedTime > _blinkInterval)
                {
                    elapsedTime = 0;
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}