using Zenject;

public class SceneInstaller : MonoInstaller
{
    // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {
        //TODO: ask about it 
        Container.Bind<InputKeyAndMouse>().AsSingle();
        Container.Bind<IInputProvider>().To<InputKeyAndMouse>().FromResolve();
        
        Container.BindFactory<CharacterModel, PlayerModelFactory>().AsSingle();
        Container.BindFactory<CharacterView, PlayerViewFactory>().AsSingle();

        Container.Bind<CharacterService>().AsSingle();
        Container.Bind<CharacterConfig>().AsSingle();

        Container.Bind<CharacterStorage>().AsSingle();
        Container.Bind<ViewStorage>().AsSingle();
    }
}