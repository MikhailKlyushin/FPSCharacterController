using System;
using UnityEngine;

public class InicializationPlayer : MonoBehaviour
{
    [SerializeField] private CharacterView _characterView;
    [SerializeField] private CharacterCameraView _characterCamera;
    [SerializeField] private Transform targetForCamera;

    private readonly CharacterService _playerService = new CharacterService();
    private CharacterModel _playerModel;

    void Start()
    {
        _playerModel = _playerService.CreatePlayer();
        _characterView.SetModel(_playerModel);

        _characterCamera.SetModel(_playerModel);
        _characterCamera.SetTarget(targetForCamera);
    }
}
