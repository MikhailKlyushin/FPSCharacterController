using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configuration Script/Character Config", order = 1)]
public class CharacterConfig : ScriptableObject
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _angularSpeed;

    public float MoveSpeed => _moveSpeed;
    public float AngularSpeed => _angularSpeed;
}
