/*
using Cinemachine;
using UnityEngine;
using Zenject;

public class CameraFolow : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
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
        _camera.Follow = playerView.transform;
        //var cameraView = _playerService.CreatePlayerCamera(playerView.transform);
    }
}
*/
