using UnityEngine;

public class InitializationPlayer : MonoBehaviour
{
    [SerializeField] private CharacterCameraView characterCamera;

    private readonly CharacterService _playerService = new CharacterService();

    private void Start()
    {
        CameraTrackingMode();
    }

    private void CameraTrackingMode()
    {
        var playerInput = new InputKeyAndMouse();
        var playerID = _playerService.CreatePlayer(playerInput);
        
        var playerModel = _playerService.GetModel(playerID);
        var playerView = _playerService.GetView(playerID);
        
        var cameraView = Instantiate(characterCamera);
        cameraView.SetInput(playerInput);
        cameraView.SetTarget(playerView.transform);
    }
}