using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Installers Script/Game Settings", order = 0)]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private ViewRegistry viewRegistry;
    [SerializeField] private CameraConfig cameraConfig;


    public override void InstallBindings()
    {
        Container.BindInstance(_characterConfig);
        Container.BindInstance(viewRegistry);
        Container.BindInstance(cameraConfig);
    }
}

