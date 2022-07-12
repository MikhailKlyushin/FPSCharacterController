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
        var prefabCharacter = Resources.Load<GameObject>(_config.PathToPrefab);
        var view = _container.InstantiatePrefabForComponent<CharacterView>(prefabCharacter);
        view.SetModel(model);

        return view;
    }
}
