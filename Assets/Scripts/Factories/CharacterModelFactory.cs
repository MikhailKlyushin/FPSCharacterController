using Zenject;

public class CharacterModelFactory : PlaceholderFactory<CharacterModel>
{
    private readonly DiContainer _container;

    public CharacterModelFactory(DiContainer container)
    {
        _container = container;
    }

    public override CharacterModel Create()
    {
        return _container.Instantiate<CharacterModel>();
    }
}