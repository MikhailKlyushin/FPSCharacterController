public interface ICharacterFactory
{
    public CharacterModel Create(IInputProvider input, CharacterConfig config);
}
