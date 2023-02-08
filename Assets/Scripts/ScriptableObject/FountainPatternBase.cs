using System.Collections;
using UnityEngine;
public abstract class FountainPatternBase : ScriptableObject
{
    public abstract IEnumerator CO_Behaviour(FountainSettings fountainSettings);
    protected void CreateAndPush(FountainSettings fountainSettings, Vector3 spawnPosition, Vector3 forceDirection)
    {
        var collectable = LevelManagerBase.SetCollectableProperties(spawnPosition, fountainSettings.CollectableScale, fountainSettings.GetRandomCollectableColor);
        
        PushCollectable(collectable, forceDirection * fountainSettings.SpawnForce);
    }

    protected void PushCollectable(GameObject collectable, Vector3 force)
    {
        if (collectable.TryGetComponent(out IPushable pushable))
        {
            pushable.Push(force);
        }
    }
}