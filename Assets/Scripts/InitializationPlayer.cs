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
        //TODO: use Zenject!
        var playerInput = new InputKeyAndMouse();
        
        var playerID = _playerService.CreatePlayer(playerInput);

        var playerView = _playerService.GetView(playerID);
        
        //TODO: spawn from Zenject's factory
        var cameraView = Instantiate(characterCamera);
        
        cameraView.SetInput(playerInput);
        cameraView.SetTarget(playerView.transform);
    }
}