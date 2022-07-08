using UnityEngine;
using Zenject;

public class PlayerViewFactory : PlaceholderFactory<CharacterView>
{
    private readonly DiContainer _container;
    const string pathToPrefab = "Prefabs/CharacterSWAT";
    

    public PlayerViewFactory(DiContainer container)
    {
        _container = container;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public CharacterView Create(CharacterModel model)
    {
        var prefabCharacter = Resources.Load<GameObject>(pathToPrefab);
        var view = _container.InstantiatePrefabForComponent<CharacterView>(prefabCharacter);
        view.SetModel(model);

        return view;
    }
}
