using System;
using UnityEngine;
using Zenject;


public class CharacterService
{
    private readonly CharacterStorage _characterStorage = new CharacterStorage();
    private readonly ViewStorage _viewStorage = new ViewStorage();

    //TODO: It's not using
    public CharacterModel GetChatacter(Guid characterID)
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


    public CharacterModel CreatePlayer()
    {
        string pathToPlayerconfig = "Config/PlayerConfig";
        CharacterConfig playerConfig = Resources.Load<CharacterConfig>(pathToPlayerconfig);

        InputKeyAndMouse playerInput = new InputKeyAndMouse();

        PlayerFactory playerFactory = new PlayerFactory();
        CharacterModel playerModel = playerFactory.CreateCharacter(playerInput, playerConfig);

        _characterStorage.AddCharacterModel(playerModel);

        return playerModel;
    }

    public CharacterView CreateView()
    {
        string pathToPrefab = "Prefabs/CharacterSWAT";
        CharacterView prefabView = Resources.Load<CharacterView>(pathToPrefab);

        _viewStorage.AddCharacterView(prefabView);

        return prefabView;
    }
}