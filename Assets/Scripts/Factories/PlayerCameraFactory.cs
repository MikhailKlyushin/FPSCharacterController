using UnityEngine;
using Zenject;

public class PlayerCameraFactory : PlaceholderFactory<CharacterCameraView>
{
    private readonly DiContainer _container;
    private CameraConfig _config;

    public PlayerCameraFactory(DiContainer container, CameraConfig config)
    {
        _container = container;
        _config = config;
    }

    public CharacterCameraView Create(Transform target)
    {
        var prefabCamera = Resources.Load<GameObject>(_config.PathToPrefab);
        var view = _container.InstantiatePrefabForComponent<CharacterCameraView>(prefabCamera);
        view.SetTarget(target);

        return view;
    }
}
