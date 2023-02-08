using UnityEngine;

public interface ICollectable
{
    public bool IsCollected();
    public void Collect(CollectableSuckerSettings collectableSuckerSettings, Vector3 collectPosition);
}