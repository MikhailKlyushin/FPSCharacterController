using UnityEngine;

public class CharacterViewFactory : IViewFactory
{
    public GameObject CreateView(CharacterModel model, GameObject viewPrefab)
    {
        //TODO: What is the shit??? GameObject.Instantiate(prefab)
        var view = new CharacterView();
        GameObject observeObject = view.InicializateView(model, viewPrefab);
        return observeObject;
    }
}