using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "Printer Data", order = 0)]
public class PrinterData : ScriptableObject
{
    public ImageSettings[] ImageSettings;
}


[System.Serializable]
public struct ImageSettings
{
    public Texture2D Texture2D;
    public float CubeScale;
}