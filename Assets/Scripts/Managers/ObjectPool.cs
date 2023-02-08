using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [SerializeField] private ObjectPoolSettings _objectPoolSettings;

    private readonly Stack<GameObject> _pool = new Stack<GameObject>();

    private int _totalCount;

    private void Awake()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < _objectPoolSettings.PoolAmount; i++)
        {
            CreateObject();
        }
    }

    private void CreateObject()
    {
        var spawnedObject = Instantiate(_objectPoolSettings.ObjectToPool, transform);
        spawnedObject.SetActive(false);
        _pool.Push(spawnedObject);

        spawnedObject.name = $"Cube {_totalCount++.ToString()}";
    }

    public void ReturnToPool(GameObject objectToPool)
    {
        objectToPool.SetActive(false);
        _pool.Push(objectToPool);
    }

    public GameObject GetObject()
    {
        if (_pool.Count == 0)
        {
            CreateObject();
        }

        var pulledObject = _pool.Pop();
        pulledObject.SetActive(true);
        return pulledObject;
    }
}