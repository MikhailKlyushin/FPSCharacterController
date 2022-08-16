using UnityEngine;
using Zenject;

public class InitializationPlayer : MonoBehaviour
{
    private CharacterService _characterService;
    
    [Inject]
    public void Construct(CharacterService service)
    {
        _characterService = service;
    }

    private void Start()
    {
        CameraTrackingMode();
    }

    private void CameraTrackingMode()
    {
        var playerID = _characterService.CreateSinglePlayer();
        var playerView = _characterService.GetView(playerID);
        var cameraView = _characterService.CreatePlayerCamera(playerView.transform);
    }
}