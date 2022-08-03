using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(IInputProvider), typeof(IInitializable)).To<InputKeyAndMouse>().AsSingle();

        Container.BindFactory<CharacterModel, CharacterModelFactory>().AsSingle();
        Container.BindFactory<CharacterView, CharacterViewFactory>().AsSingle();
        Container.BindFactory<CharacterCameraView, PlayerCameraFactory>().AsSingle();

        Container.Bind<CharacterService>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<Storage<CharacterModel>>().AsSingle();
        Container.BindInterfacesAndSelfTo<Storage<CharacterView>>().AsSingle();

        Container.BindInterfacesTo<CreateClientPlayerNetworkRule>().AsSingle();
    }
}