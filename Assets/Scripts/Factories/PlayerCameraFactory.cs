using UnityEngine;
using Zenject;

public class PlayerCameraFactory : PlaceholderFactory<CharacterCameraView>
{
    private readonly DiContainer _container;
    
    //TODO: move to config
    private const string PathToPrefab = "Prefabs/ThirdPersonCamera";
    

    public PlayerCameraFactory(DiContainer container)
    {
        _container = container;
    }

    //TODO: Do you need that?)
    // ReSharper disable Unity.PerformanceAnalysis
    
    public CharacterCameraView Create(Transform target)
    {
        var prefabCamera = Resources.Load<GameObject>(PathToPrefab);
        var view = _container.InstantiatePrefabForComponent<CharacterCameraView>(prefabCamera);
        view.SetTarget(target);

        return view;
    }
}
