using UnityEngine;


[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configuration Script/Character Config", order = 2)]

public class CharacterConfig : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private float _sensivityHorizontal = 20f;
    
    public float MoveSpeed => _moveSpeed;
    public float SensivityHorizontal => _sensivityHorizontal;
}
