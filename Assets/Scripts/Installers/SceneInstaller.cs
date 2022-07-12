using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignalWithInterfaces<SignalInputProvider>();

        Container.Bind(typeof(IInputProvider), typeof(ITickable)).To<InputKeyAndMouse>().AsSingle();

        Container.BindFactory<CharacterModel, PlayerModelFactory>().AsSingle();
        Container.BindFactory<CharacterView, PlayerViewFactory>().AsSingle();
        Container.BindFactory<CharacterCameraView, PlayerCameraFactory>().AsSingle();

        Container.Bind<CharacterService>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<Storage<CharacterModel>>().AsSingle();
        Container.BindInterfacesAndSelfTo<Storage<CharacterView>>().AsSingle();
    }
}