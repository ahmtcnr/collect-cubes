using UnityEngine;

public class Node : MonoBehaviour
{
    private int _collectableCount;
    public Vector2Int GridIndex;


    public int CollectableCount
    {
        get => _collectableCount;
        private set
        {
            _collectableCount = value;
            if (_collectableCount < 0)
            {
                _collectableCount = 0;
            }
        }
    }

    public bool IsWalkable => _isWalkable;


    private bool _isWalkable = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectable collectable) && !collectable.IsCollected())
        {
            CollectableCount++;
        }

        if (other.TryGetComponent(out Obstacle obstacle))
        {
            _isWalkable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ICollectable collectable))
        {
            CollectableCount--;
        }
    }
}