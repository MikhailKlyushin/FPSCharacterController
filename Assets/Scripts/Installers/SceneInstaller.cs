using Zenject;

public class SceneInstaller : MonoInstaller
{
    // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {
        Container.Bind<IInputProvider>().WithId("id").To<InputKeyAndMouse>().FromResolve();
        Container.BindInterfacesAndSelfTo<InputKeyAndMouse>().AsSingle();

        Container.BindFactory<CharacterModel, PlayerModelFactory>().AsSingle();
        Container.BindFactory<CharacterView, PlayerViewFactory>().AsSingle();
        Container.BindFactory<CharacterCameraView, PlayerCameraFactory>().AsSingle();

        Container.Bind<CharacterService>().AsSingle();

        Container.Bind<CharacterStorage>().AsSingle();
        Container.Bind<ViewStorage>().AsSingle();
    }
}