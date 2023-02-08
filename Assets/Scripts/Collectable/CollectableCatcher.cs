using UnityEngine;
public class CollectableCatcher : MonoBehaviour
{
    [SerializeField] private Vector3 _reSpawnPosition;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectable collectable))
        {
            other.transform.position = _reSpawnPosition;
        }
    }
}