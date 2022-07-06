public interface ICharacterFactory
{
    public CharacterModel CreateCharacter(IInputProvider input, CharacterConfig config);
}
