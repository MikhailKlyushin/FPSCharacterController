using UnityEngine;
using Zenject;

public class PlayerCameraFactory : PlaceholderFactory<CharacterCameraView>
{
    private readonly DiContainer _container;
    private readonly CameraConfig _config;

    public PlayerCameraFactory(DiContainer container, CameraConfig config)
    {
        _container = container;
        _config = config;
    }

    public CharacterCameraView Create(Transform target)
    {
        var view = _container.InstantiatePrefabForComponent<CharacterCameraView>(_config.CameraPrefab);
        view.SetTarget(target);

        return view;
    }
}
