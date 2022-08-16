using UnityEngine;

[CreateAssetMenu(fileName = "ViewRegistry", menuName = "Configuration Script/View Registry", order = 4)]

public class ViewRegistry : ScriptableObject
{
    [SerializeField] private GameObject _characterPrefab;
    
    public GameObject CharacterPrefab => _characterPrefab;
}
