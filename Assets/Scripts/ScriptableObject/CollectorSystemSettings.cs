using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Movement Data", order = 0)]
public class CollectorSystemSettings : ScriptableObject
{
    [Header("Collector")] [SerializeField] private float _collectorSpeed;
    [SerializeField] private float _acceptedDistance;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Color _collectorColor;


    [Header("Ghost Object")] [SerializeField]
    private float _ghostObjectResponseTime;

    [SerializeField] private Vector3 _frameSize;

    [Header("Sucker")] [SerializeField] private Vector3 _suckerPosition;

    public Vector3 SuckerPosition => _suckerPosition;
    public float CollectorSpeed => _collectorSpeed;
    public float AcceptedDistance => _acceptedDistance;
    public Vector3 StartPosition => _startPosition;
    public float GhostObjectResponseTime => _ghostObjectResponseTime;
    public Vector3 FrameSize => _frameSize;
    public Vector3 StartDirection => (_suckerPosition - _startPosition).normalized;
    public Color CollectorColor => _collectorColor;
}