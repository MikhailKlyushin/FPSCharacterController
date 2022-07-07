using System.Runtime.Serialization;
using System.Xml.Xsl;
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
        var playerModel = _playerService.CreatePlayer();
        var loadView = _playerService.CreateView();
        var playerView = Instantiate(loadView, new Vector3(), new Quaternion()) as CharacterView;
        playerView.SetModel(playerModel);
        _playerService.AddViewStorage(playerView);


        ////Refactor this!
        var camera = Instantiate(_characterCamera);
        camera.SetModel(playerModel);

        ////TODO: To get player's view you need put it in ViewStorage, then get model's id and get view by this id
        camera.SetTarget(playerView.transform);
    }
}