using UnityEngine;
using Zenject;

public class CharacterViewFactory : PlaceholderFactory<CharacterView>
{
    private readonly DiContainer _container;
    private readonly ViewRegistry _registry;


    public CharacterViewFactory(DiContainer container, ViewRegistry registry)
    {
        _container = container;
        _registry = registry;
    }

    public CharacterView Create(CharacterModel model)
    {
        var view = _container.InstantiatePrefabForComponent<CharacterView>(_registry.CharacterPrefab);
        view.SetModel(model);

        return view;
    }
}
