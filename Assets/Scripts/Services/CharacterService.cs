using System;
using UnityEngine;


public class CharacterService
{
    private readonly CharacterStorage _characterStorage;
    private readonly ViewStorage _viewStorage;

    private readonly PlayerModelFactory _playerModelFactory;
    private readonly PlayerViewFactory _playerViewFactory;
    private readonly PlayerCameraFactory _playerCameraFactory;

    CharacterService
    (
        PlayerModelFactory playerModelFactory, 
        PlayerViewFactory playerViewFactory,
        PlayerCameraFactory playerCameraFactory,
        CharacterStorage characterStorage, 
        ViewStorage viewStorage
    )
    {
        _characterStorage = characterStorage;
        _viewStorage = viewStorage;
        
        _playerModelFactory = playerModelFactory;
        _playerViewFactory = playerViewFactory;
        _playerCameraFactory = playerCameraFactory;
    }
    
    public CharacterModel GetModel(string characterID)
    {
        var model = _characterStorage.GetChatacterModel(characterID);

        if (model != null)
        {
            return model;
        }
        
        Debug.Log("CharacterStorage.cs: Character with this ID not found!");
        return null;
    }

    public CharacterView GetView(string characterID)
    {
        var view = _viewStorage.GetChatacterView(characterID);

        if (view != null)
        {
            return view;
        }
        
        Debug.Log("Exception! - CharacterStorage.cs: Character with this ID not found!");
        return null;
    }

    public string CreatePlayer()
    {
        var model = CreateModel();
        var view = CreateView(model);

        return view.CharacterID;
    }


    public CharacterModel CreateModel()
    {
        const string pathToPlayerConfig = "Config/PlayerConfig";
        var playerConfig = Resources.Load<CharacterConfig>(pathToPlayerConfig);
        var playerModel = _playerModelFactory.Create();

        _characterStorage.AddCharacterModel(playerModel);

        return playerModel;
    }

    public CharacterView CreateView(CharacterModel model)
    {
        var playerView = _playerViewFactory.Create(model);
        _viewStorage.AddCharacterView(playerView);

        return playerView;
    }

    public CharacterCameraView CreatePlayerCamera(Transform target)
    {
        var playerCamera = _playerCameraFactory.Create(target);
        return playerCamera;
    }
}