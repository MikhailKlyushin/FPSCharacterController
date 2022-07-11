using UnityEngine;


[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configuration Script/Character Config", order = 1)]

public class CharacterConfig : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private float _sensivityHorisontal = 20f;
    
    public float MoveSpeed => _moveSpeed;
    public float SensivityHorizontal => _sensivityHorisontal;
}
