using UnityEngine;

//TODO: Factory is factory. It exist Create method only for models and views.
public interface IViewFactory
{
    public GameObject CreateView(CharacterModel model ,GameObject prefab);
}