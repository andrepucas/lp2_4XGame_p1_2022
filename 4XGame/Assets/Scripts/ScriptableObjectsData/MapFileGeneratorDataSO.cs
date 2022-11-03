using UnityEngine;

[CreateAssetMenu(fileName = "MapFileGeneratorData", menuName = "Data/Map File Generator Data")]
public class MapFileGeneratorDataSO : ScriptableObject
{
    // Serialized
    [Header("Map Generator Data")]
    [SerializeField] private string[] _terrains;
    [SerializeField] private string[] _resources;

    // Read-only Properties
    public string[] Terrains => _terrains;
    public string[] Resources => _resources;
    
}
