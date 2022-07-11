using UnityEngine;
using Zenject;

public class InitializationPlayer : MonoBehaviour
{
    [SerializeField] private CharacterCameraView characterCamera;
    
    private CharacterService _playerService;
    
    [Inject]
    public void Construct(CharacterService service)
    {
        _playerService = service;
    }
    
    private void Start()
    {
        CameraTrackingMode();
    }
    
    private void CameraTrackingMode()
    {
        var playerID = _playerService.CreatePlayer();
        var playerView = _playerService.GetView(playerID);
        var cameraView = _playerService.CreatePlayerCamera(playerView.transform);
    }
}