using UnityEngine;

public class PlayerViewFactory : IViewFactory
{
    public CharacterView Create()
    {
        const string pathToPrefab = "Prefabs/CharacterSWAT";
        var prefabView = Resources.Load<CharacterView>(pathToPrefab);
        var view = Object.Instantiate(prefabView, new Vector3(), new Quaternion()) as CharacterView;

        return view;
    }
}
