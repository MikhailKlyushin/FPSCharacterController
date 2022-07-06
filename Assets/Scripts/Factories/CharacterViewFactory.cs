using UnityEngine;

public class CharacterViewFactory : IViewFactory
{
    public GameObject CreateView(CharacterModel model, GameObject viewPrefab)
    {
        var view = new CharacterView();
        GameObject observeObject = view.InicializateView(model, viewPrefab);
        return observeObject;
    }
}