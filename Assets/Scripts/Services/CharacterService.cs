using UnityEngine;


public class CharacterService
{
    private readonly Storage<CharacterModel> _characterStorage;
    private readonly Storage<CharacterView> _viewStorage;

    private readonly PlayerModelFactory _playerModelFactory;
    private readonly PlayerViewFactory _playerViewFactory;
    private readonly PlayerCameraFactory _playerCameraFactory;

    private CharacterService
    (
        Storage<CharacterModel> characterStorage, 
        Storage<CharacterView> viewStorage,
        PlayerModelFactory playerModelFactory, 
        PlayerViewFactory playerViewFactory,
        PlayerCameraFactory playerCameraFactory)
    {
        _characterStorage = characterStorage;
        _viewStorage = viewStorage;
        
        _playerModelFactory = playerModelFactory;
        _playerViewFactory = playerViewFactory;
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

    public CharacterModel CreateAndGetModelForNetworkPlayer()
    {
        return CreateModel();
    }

    private CharacterModel CreateModel()
    {
        var playerModel = _playerModelFactory.Create();

        _characterStorage.Add(playerModel);

        return playerModel;
    }

    private CharacterView CreateView(CharacterModel model)
    {
        var playerView = _playerViewFactory.Create(model);
        _viewStorage.Add(playerView);

        return playerView;
    }
    
}