using UnityEngine;

public class Scoop : MonoBehaviour
{
    [SerializeField] private LayerMask _scoopLayer;
    [SerializeField] private LayerMask _defaultLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectable collectable) && !collectable.IsCollected())
        {
            other.gameObject.layer = _scoopLayer.GetLayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ICollectable collectable) && !collectable.IsCollected())
        {
            other.gameObject.layer = _defaultLayer.GetLayer();
        }
    }
}