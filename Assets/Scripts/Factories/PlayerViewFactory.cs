using UnityEngine;
using Zenject;

public class PlayerViewFactory : PlaceholderFactory<CharacterView>
{
    private readonly DiContainer _container;
    private const string PathToPrefab = "Prefabs/CharacterSWAT";
    

    public PlayerViewFactory(DiContainer container)
    {
        _container = container;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public CharacterView Create(CharacterModel model)
    {
        var prefabCharacter = Resources.Load<GameObject>(PathToPrefab);
        var view = _container.InstantiatePrefabForComponent<CharacterView>(prefabCharacter);
        view.SetModel(model);

        return view;
    }
}
