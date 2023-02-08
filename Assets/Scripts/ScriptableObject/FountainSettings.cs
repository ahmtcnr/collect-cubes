using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(menuName = "Fountain Setting")]
public class FountainSettings : ScriptableObject
{
    public FountainPatternBase _fountainPattern;


    [SerializeField] private float _countdownTime;
    [SerializeField] private Vector3 _spawnPosition;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _spawnForce;
    [SerializeField] private int _maximumCollectableAmount;
    [SerializeField] private float _collectableScale;
    [SerializeField] private Gradient _collectableColors;


    public float CountdownTime => _countdownTime;
    public Vector3 SpawnPosition => _spawnPosition;
    public float SpawnInterval => _spawnInterval;
    public float SpawnForce => _spawnForce;
    public int MaximumCollectableAmount => _maximumCollectableAmount;
    public float CollectableScale => _collectableScale;
    public Color GetRandomCollectableColor => _collectableColors.Evaluate(Random.Range(0f, 1f));
}