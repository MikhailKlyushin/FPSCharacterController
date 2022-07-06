using System;
using UnityEditor;
using UnityEngine;

public class InicializationPlayer : MonoBehaviour
{
    //[SerializeField] private GameObject _playerPrefab;
    [SerializeField] private CharacterCameraView _characterCamera;
    
    private Transform targetForCamera;

    private readonly CharacterService _playerService = new CharacterService();

    void Start()
    {
        var _playerModel = _playerService.CreatePlayer();
        var player = _playerService.CreateViewCharacter(_playerModel);
        //var view = _playerPrefab.GetComponent<CharacterView>();
        //view.SetModel(_playerModel);

        var camera = Instantiate(_characterCamera);

        camera.SetModel(_playerModel);
        targetForCamera = player.transform;
        camera.SetTarget(targetForCamera);
    }
}
