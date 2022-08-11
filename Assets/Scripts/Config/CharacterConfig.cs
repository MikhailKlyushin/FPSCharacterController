using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configuration Script/Character Config", order = 2)]

public class CharacterConfig : ScriptableObject
{
    [SerializeField] private short _maxHealth = 100;
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private float _sensitivityHorizontal = 20f;
    [SerializeField] private float _smoothRotate= 0.8f;

    public short MaxHealth => _maxHealth;
    public float MoveSpeed => _moveSpeed;
    public float SensitivityHorizontal => _sensitivityHorizontal;
    public float SmoothRotate => _smoothRotate;
}
