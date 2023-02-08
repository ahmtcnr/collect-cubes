using UnityEngine;

public static class LayerMaskToLayer
{
    public static int GetLayer(this LayerMask layerMask) => Mathf.FloorToInt(Mathf.Log(layerMask.value, 2));
}