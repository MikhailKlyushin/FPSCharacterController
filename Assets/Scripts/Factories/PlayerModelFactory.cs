public class PlayerModelFactory : ICharacterFactory
{
    public CharacterModel Create(IInputProvider input, CharacterConfig config)
    {
        CharacterModel model = new CharacterModel(input, config);

        return model;
    }
}