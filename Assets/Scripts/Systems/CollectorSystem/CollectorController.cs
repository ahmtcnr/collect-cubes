using UnityEngine;

public class CollectorController : MonoBehaviour, ICollector
{
    private Rigidbody _rb;
    private Vector3 _totalDeltaInput;


    [SerializeField] private CollectorControllerBase _collectorController;
    [SerializeField] private CollectorSystemSettings _collectorSystemSettings;
    [SerializeField] private Transform _ghostTransform;

    [SerializeField] private Renderer _collectorRenderer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collectorRenderer.material.SetColor("_BaseColor", _collectorSystemSettings.CollectorColor);
    }

    private void OnEnable()
    {
        _collectorController.OnPointerDrag += UpdatePlayerInputDelta;

        Actions.OnGameStateChanged += HandleGameState;
    }

    private void OnDisable()
    {
        _collectorController.OnPointerDrag -= UpdatePlayerInputDelta;

        Actions.OnGameStateChanged -= HandleGameState;
    }

    private void HandleGameState(GameState state)
    {
        switch (state)
        {
            case GameState.GenerateLevel:
                SetCollectorDefaultPosition();
                break;
        }
    }

    private void SetCollectorDefaultPosition()
    {
        _ghostTransform.position = transform.position = _collectorSystemSettings.StartPosition;
        transform.rotation = Quaternion.Euler(_collectorSystemSettings.StartDirection);
    }

    private void UpdatePlayerInputDelta(Vector3 inputDelta) => _totalDeltaInput += inputDelta;


    private void FixedUpdate()
    {
        MoveGhost();
        FollowGhost();
        RotateCollector();
    }


    private void MoveGhost()
    {
        var pace = _totalDeltaInput * (_collectorSystemSettings.GhostObjectResponseTime * Time.deltaTime);
        var targetPosition = _ghostTransform.position + pace;


        _ghostTransform.position = ClampToBorder(targetPosition);
        _totalDeltaInput -= pace;
    }

    private void FollowGhost()
    {
        Vector3 velocityToGhost = Vector3.zero;
        
        var distanceToGhost = (_ghostTransform.position - transform.position).sqrMagnitude;
        if (distanceToGhost > _collectorSystemSettings.AcceptedDistance && velocityToGhost.magnitude < 0.5f)
        {
            velocityToGhost = (_ghostTransform.position - transform.position) * (_collectorSystemSettings.CollectorSpeed * Time.deltaTime);
        }

       


        _rb.velocity = velocityToGhost;
    }

    private void RotateCollector()
    {
        if (_rb.velocity == Vector3.zero)
            return;

        _rb.MoveRotation(Quaternion.LookRotation(_rb.velocity.normalized));
    }

    private Vector3 ClampToBorder(Vector3 targetPosition)
    {
        targetPosition.x = Mathf.Clamp(targetPosition.x, -_collectorSystemSettings.FrameSize.x, _collectorSystemSettings.FrameSize.x);
        targetPosition.z = Mathf.Clamp(targetPosition.z, -_collectorSystemSettings.FrameSize.z, _collectorSystemSettings.FrameSize.z);
        targetPosition.y = 0;
        return targetPosition;
    }

    public void Respawn()
    {
        SetCollectorDefaultPosition();
    }
}