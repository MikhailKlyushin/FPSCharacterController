using UnityEngine;

public class InicializationPlayer : MonoBehaviour
{
    [SerializeField] private CharacterCameraView _characterCamera;

    private readonly CharacterService _playerService = new CharacterService();

    void Start()
    {
        CameraTrackingMode();
    }

    private void CameraTrackingMode()
    {
        var _playerModel = _playerService.CreatePlayer();
        
        //TODO: It must be in _playerService.CreatePlayer method
        var player = _playerService.CreateViewCharacter(_playerModel);

        var camera = Instantiate(_characterCamera);
        
        //Refactor this!
        camera.SetModel(_playerModel);
        
        //TODO: To get player's view you need put it in ViewStorage, then get model's id and get view by this id
        camera.SetTarget(player.transform);
    }

    //TODO: It's not using
    private void FreeCameraMode()
    {
        var camera = Instantiate(_characterCamera);
        camera.SetInput(new InputKeyAndMouse());
    }
}
