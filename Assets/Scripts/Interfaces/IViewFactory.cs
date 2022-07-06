using UnityEngine;

public interface IViewFactory
{
    public GameObject CreateView(CharacterModel model ,GameObject prefab);
}