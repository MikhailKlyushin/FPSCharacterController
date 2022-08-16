using UnityEngine;
using Zenject;

public class PlayerCameraFactory : PlaceholderFactory<CharacterCameraView>
{
    private readonly DiContainer _container;
    private readonly CameraConfig _cameraConfig;

    public PlayerCameraFactory(DiContainer container, CameraConfig cameraConfig)
    {
        _container = container;
        _cameraConfig = cameraConfig;
    }

    public CharacterCameraView Create(Transform target)
    {
        var view = _container.InstantiatePrefabForComponent<CharacterCameraView>(_cameraConfig.CameraPrefab);
        view.SetTarget(target);

        return view;
    }
}
