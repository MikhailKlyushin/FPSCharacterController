using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicializationPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private Transform _spawnPoints;

    private CharacterService _playerService = new CharacterService();
    private CharacterModel _characterModel;
    private CharacterView _characterView;

    void Start()
    {
        _characterModel = _playerService.CreatePlayer();
        //_characterView = _playerService.AddCharacterView(_characterModel);
        var view = _player.GetComponent<CharacterView>();
        view = _characterView;
        Instantiate(_player, _spawnPoints);
        //_viewComponent = GetComponent<CharacterViewComponent>();
        //_viewComponent.SetCharacterView(_characterView);
    }

   
}
