using Zenject;

public class PlayerModelFactory : PlaceholderFactory<CharacterModel>
{
    private readonly DiContainer _container;

    public PlayerModelFactory(DiContainer container)
    {
        _container = container;
    }

    public override CharacterModel Create()
    {
        return _container.Instantiate<CharacterModel>();
    }
}