using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private readonly CharacterService _characterService = new CharacterService();
    private ICharacter _character;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _character = _characterService.CreatePlayer();
    }
    void Update()
    {
        _rigidbody.velocity = _character.CharacterModel.Velocity;
    }
}
