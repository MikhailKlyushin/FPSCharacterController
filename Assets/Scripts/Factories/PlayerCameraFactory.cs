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
        //TODO: _container.InstantiatePrefabResourceForComponent<CharacterCameraView>(_config.PathToPrefab);
        //but better use config with prefab reference and resolve it here and create from container through _container.InstantiatePrefab
        var prefabCamera = Resources.Load<GameObject>(_config.PathToPrefab);
        var view = _container.InstantiatePrefabForComponent<CharacterCameraView>(prefabCamera);
        view.SetTarget(target);

        return view;
    }
}
