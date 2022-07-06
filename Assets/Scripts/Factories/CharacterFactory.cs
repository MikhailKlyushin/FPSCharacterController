public class PlayerFactory : ICharacterFactory
{
    public CharacterModel CreateCharacter(IInputProvider input, CharacterConfig config)
    {
        CharacterModel model = new CharacterModel(input, config);

        return model;
    }
}