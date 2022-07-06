using System;
using UnityEngine;


public class CharacterService
{
    private readonly CharacterStorage _storage = new CharacterStorage();

    //TODO: It's not using
    public CharacterModel GetChatacter(Guid characterID)
    {
        return _storage.GetChatacterModel(characterID);
    }


    public CharacterModel CreatePlayer()
    {
        string pathToPlayerconfig = "Config/PlayerConfig";
        CharacterConfig playerConfig = Resources.Load<CharacterConfig>(pathToPlayerconfig);

        InputKeyAndMouse playerInput = new InputKeyAndMouse();

        PlayerFactory playerFactory = new PlayerFactory();
        CharacterModel playerModel = playerFactory.CreateCharacter(playerInput, playerConfig);


        _storage.AddCharacterModel(playerModel);

        return playerModel;
    }

    public GameObject CreateViewCharacter(CharacterModel playerModel)
    {
        string pathToPrefab = "Prefabs/CharacterSWAT";
        
        //TODO: For what did you create the ViewConfig?
        GameObject prefab = Resources.Load<GameObject>(pathToPrefab);

        CharacterViewFactory viewFactory = new CharacterViewFactory();

        return viewFactory.CreateView(playerModel, prefab);
    }
}