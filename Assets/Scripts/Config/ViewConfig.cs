using UnityEngine;

[CreateAssetMenu(fileName = "ViewConfig", menuName = "Configuration Script/View Config", order = 4)]

public class ViewConfig : ScriptableObject
{
    [SerializeField] private GameObject _characterPrefab;
    
    public GameObject CharacterPrefab => _characterPrefab;
}
