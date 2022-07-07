using System;
using UnityEngine;
using Object = UnityEngine.Object;


public class CharacterService
{
    private readonly CharacterStorage _characterStorage = new CharacterStorage();
    private readonly ViewStorage _viewStorage = new ViewStorage();
    
    public CharacterModel GetModel(Guid characterID)
    {
        return _characterStorage.GetChatacterModel(characterID);
    }

    public CharacterView GetView(Guid characterID)
    {
        return _viewStorage.GetChatacterModel(characterID);
    }

    public void AddViewStorage(CharacterView view)
    {
        _viewStorage.AddCharacterView(view);
    }

    public Guid CreatePlayer(IInputProvider playerInput)
    {
        var model = CreateModel(playerInput);
        var view = CreateView(model);

        return model.CharacterID;
    }


    public CharacterModel CreateModel(IInputProvider playerInput)
    {
        const string pathToPlayerConfig = "Config/PlayerConfig";
        var playerConfig = Resources.Load<CharacterConfig>(pathToPlayerConfig);
        var modelFactory = new PlayerModelFactory();
        var playerModel = modelFactory.Create(playerInput, playerConfig);

        _characterStorage.AddCharacterModel(playerModel);
        
        return playerModel;
    }

    public CharacterView CreateView(CharacterModel model)
    {
        var viewFactory = new PlayerViewFactory();
        var playerView = viewFactory.Create();
        playerView.SetModel(model);
        
        _viewStorage.AddCharacterView(playerView);

        return playerView;
    }
}