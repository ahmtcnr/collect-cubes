using UnityEngine;


[CreateAssetMenu(menuName = "Collect Animation Settings", order = 0)]
public class CollectableSuckerSettings : ScriptableObject
{
    [SerializeField] private float _delayTimeToInactivation;
    [SerializeField] private float _collectionForce;
    [SerializeField] private LayerMask _layerAfterCollection;
    [SerializeField] private Color _collectColor;


    public float DelayTimeToInactivation => _delayTimeToInactivation;
    public float CollectionForce => _collectionForce;
    public LayerMask LayerAfterCollection => _layerAfterCollection;
    public Color CollectColor => _collectColor;
}