using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Installers Script/Game Settings", order = 0)]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private ViewConfig _viewConfig;
    [SerializeField] private CameraConfig _cameraConfig;


    public override void InstallBindings()
    {
        Container.BindInstance(_characterConfig);
        Container.BindInstance(_viewConfig);
        Container.BindInstance(_cameraConfig);
    }
}

