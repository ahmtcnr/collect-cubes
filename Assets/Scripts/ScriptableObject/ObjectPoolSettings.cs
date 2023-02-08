using UnityEngine;


[CreateAssetMenu(fileName = "Object Pool", menuName = "Settings")]
public class ObjectPoolSettings : ScriptableObject
{
    public GameObject ObjectToPool;
    public int PoolAmount;
}