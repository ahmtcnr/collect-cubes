using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : CollectorControllerBase, IDragHandler, IPointerDownHandler, IPointerExitHandler
{
    private Vector3 _lastPos = Vector3.zero;

    private bool _isEnabled;
    private void EnableInput() => _isEnabled = true;
    private void DisableInput() => _isEnabled = false;

    protected override void HandleGameState(GameState state)
    {
        switch (state)
        {
            case GameState.StartGame:
                EnableInput();
                break;
            case GameState.EndGame:
                DisableInput();
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Actions.OnPointerDown?.Invoke();
        _lastPos = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isEnabled) return;

        var worldDelta = eventData.pointerCurrentRaycast.worldPosition - _lastPos;
        OnPointerDrag?.Invoke(worldDelta);
        _lastPos = eventData.pointerCurrentRaycast.worldPosition;
    }
    public override event Action<Vector3> OnPointerDrag;

    public void OnPointerExit(PointerEventData eventData)
    {
        _lastPos = Vector3.zero;
    }
}