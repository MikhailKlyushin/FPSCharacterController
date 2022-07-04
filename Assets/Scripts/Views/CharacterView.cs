using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour
{

    private readonly CharacterService _playerService = new CharacterService();
    private CharacterModel _characterModel;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _characterModel = _playerService.CreatePlayer();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _rigidbody.velocity = _characterModel.Velocity;
    }
}
