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
        var player = _playerService.CreateViewCharacter(_playerModel);

        var camera = Instantiate(_characterCamera);
        camera.SetModel(_playerModel);
        camera.SetTarget(player.transform);
    }

    private void FreeCameraMode()
    {
        var camera = Instantiate(_characterCamera);
        camera.SetInput(new InputKeyAndMouse());
    }
}
