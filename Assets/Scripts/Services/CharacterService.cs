using System;


public class CharacterService
{
    private readonly CharacterStorage _storage = new CharacterStorage();

    public CharacterModel GetChatacter(Guid characterID)
    {
        return _storage.GetChatacterModel(characterID);
    }


    public CharacterModel CreatePlayer()
    {
        PlayerFactory playerFactory = new PlayerFactory();
        CharacterModel playerModel = playerFactory.CreateCharacter();

        _storage.AddCharacterModel(playerModel);

        return playerModel;
    }
}
