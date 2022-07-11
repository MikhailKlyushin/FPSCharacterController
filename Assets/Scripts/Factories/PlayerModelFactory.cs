using Zenject;

public class PlayerModelFactory : PlaceholderFactory<CharacterModel>
{
    private readonly DiContainer _container;

    public PlayerModelFactory(DiContainer container)
    {
        _container = container;
    }

    //TODO: Do you need that?)
    // ReSharper disable Unity.PerformanceAnalysis
    
    public override CharacterModel Create()
    {
        return _container.Instantiate<CharacterModel>();
    }
}