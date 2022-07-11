using UnityEngine;

[CreateAssetMenu(fileName = "ViewConfig", menuName = "Configuration Script/View Config", order = 4)]

public class ViewConfig : ScriptableObject
{
    [SerializeField] private string _pathToPrefab = "Prefabs/CharacterSWAT";
    
    public string PathToPrefab => _pathToPrefab;
}
