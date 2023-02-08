
using UnityEngine;
public class CubeShapeHandler : MonoBehaviour,IShapeHandler
{
    [SerializeField] private Renderer _renderer;

    private MaterialPropertyBlock _materialPropertyBlock;
    private void Awake()
    {
        _materialPropertyBlock = new MaterialPropertyBlock();
    }

    public void ChangeMaterialColor(Color targetColor)
    {
        _renderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetColor("_BaseColor",targetColor);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }
    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
}