using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicializationPlayer : MonoBehaviour
{
    [SerializeField] private CharacterView _view;

    private Transform _spawnPoints;

    private CharacterService _playerService = new CharacterService();
    private CharacterModel _characterModel;

    void Start()
    {
        _characterModel = _playerService.CreatePlayer();
        _view.SetModel(_characterModel);
    }

   
}
