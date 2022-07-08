using System;
using UnityEngine;


public class CharacterService
{
    private readonly CharacterStorage _characterStorage;
    private readonly ViewStorage _viewStorage;

    private readonly PlayerModelFactory _playerModelFactory;
    private readonly PlayerViewFactory _playerViewFactory;

    CharacterService
    (
        PlayerModelFactory playerModelFactory, 
        PlayerViewFactory playerViewFactory,
        CharacterStorage characterStorage, 
        ViewStorage viewStorage
    )
    {
        _characterStorage = characterStorage;
        _viewStorage = viewStorage;
        
        _playerModelFactory = playerModelFactory;
        _playerViewFactory = playerViewFactory;
    }
    
    public CharacterModel GetModel(Guid characterID)
    {
        return _characterStorage.GetChatacterModel(characterID);
    }

    public CharacterView GetView(Guid characterID)
    {
        return _viewStorage.GetChatacterView(characterID);
    }

    public Guid CreatePlayer(IInputProvider playerInput)
    {
        var model = CreateModel(playerInput);
        var view = CreateView(model);

        return view.CharacterID;
    }


    public CharacterModel CreateModel(IInputProvider playerInput)
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
}