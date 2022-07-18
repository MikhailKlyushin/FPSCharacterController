using UnityEngine;
using Zenject;

public class PlayerViewFactory : PlaceholderFactory<CharacterView>
{
    private readonly DiContainer _container;
    private readonly ViewConfig _config;


    public PlayerViewFactory(DiContainer container, ViewConfig config)
    {
        _container = container;
        _config = config;
    }

    public CharacterView Create(CharacterModel model)
    {
        //TODO: _container.InstantiatePrefabResourceForComponent<CharacterCameraView>(_config.PathToPrefab);
        //but better use config with prefab reference and resolve it here and create from container through _container.InstantiatePrefab
        var prefabCharacter = Resources.Load<GameObject>(_config.PathToPrefab);
        var view = _container.InstantiatePrefabForComponent<CharacterView>(prefabCharacter);
        view.SetModel(model);

        return view;
    }
}
