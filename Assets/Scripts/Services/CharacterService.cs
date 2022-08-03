using UnityEngine;


public class CharacterService
{
    private readonly Storage<CharacterModel> _characterStorage;
    private readonly Storage<CharacterView> _viewStorage;

    private readonly CharacterModelFactory _characterModelFactory;
    private readonly CharacterViewFactory _characterViewFactory;
    private readonly PlayerCameraFactory _playerCameraFactory;

    private CharacterService
    (
        Storage<CharacterModel> characterStorage, 
        Storage<CharacterView> viewStorage,
        CharacterModelFactory characterModelFactory, 
        CharacterViewFactory characterViewFactory,
        PlayerCameraFactory playerCameraFactory)
    {
        _characterStorage = characterStorage;
        _viewStorage = viewStorage;
        
        _characterModelFactory = characterModelFactory;
        _characterViewFactory = characterViewFactory;
        _playerCameraFactory = playerCameraFactory;
    }
    
    public CharacterModel GetModel(string characterID)
    {
        var model = _characterStorage.Get(characterID);

        if (model != null)
        {
            return model;
        }
        
        Debug.Log("Character with this ID not found!");
        return null;
    }

    public CharacterView GetView(string characterID)
    {
        var view = _viewStorage.Get(characterID);

        if (view != null)
        {
            return view;
        }
        
        Debug.Log("View with this ID not found!");
        return null;
    }

    public CharacterCameraView CreatePlayerCamera(Transform target)
    {
        var playerCamera = _playerCameraFactory.Create(target);
        return playerCamera;
    }

    public string CreateSinglePlayer()
    {
        var model = CreateModel();
        var view = CreateView(model);

        return view.ID;
    }
    
    public GameObject AddParamsForNetworkPlayer(GameObject playerObject)
    {
        var model = CreateModel();
        var view = playerObject.GetComponent<CharacterNetworkView>();
        view.SetModel(model);

        return playerObject;
    }

    private CharacterModel CreateModel()
    {
        var model = _characterModelFactory.Create();

        _characterStorage.Add(model);

        return model;
    }

    private CharacterView CreateView(CharacterModel model)
    {
        var view = _characterViewFactory.Create(model);
        _viewStorage.Add(view);

        return view;
    }
    
}