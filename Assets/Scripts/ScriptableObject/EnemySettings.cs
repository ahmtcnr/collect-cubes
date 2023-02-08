using UnityEngine;
[CreateAssetMenu(menuName = "Enemy Settings", order = 0)]
public class EnemySettings : ScriptableObject
{
    [Header("Challenging Factor")] [Range(0f, 1f)] [SerializeField]
    private float _challengingFactor;

    public float IdleDuration => Mathf.Lerp(1, 0.3f, ChallengingFactor);
    public float ChallengingFactor => _challengingFactor;


    public float TimeDelayBetweenNodes => Mathf.Lerp(1, 0.2f, ChallengingFactor);
}