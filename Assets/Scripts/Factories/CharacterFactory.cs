public class PlayerFactory : ICharacterFactory
{
    public CharacterModel CreateCharacter()
    {
        string pathToConfigFile = "Config/PlayerConfig";

        CharacterModel model = new CharacterModel(new InputKeyAndMouse(), pathToConfigFile);

        return model;
    }
}
