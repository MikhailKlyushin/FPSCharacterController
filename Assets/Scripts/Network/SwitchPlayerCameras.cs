using Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class SwitchPlayerCameras: NetworkBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _moveCamera;
    [SerializeField] private CinemachineVirtualCamera _aimingCamera;

    private GameObject _moveCameraObject;
    
    private InputControl _inputControl;

    private bool isAimingState = false;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            _moveCamera.Follow = transform;
            _aimingCamera.Follow = transform;
            _moveCameraObject = Instantiate(_moveCamera.gameObject);
            Instantiate(_aimingCamera);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        _inputControl = new InputControl();
        _inputControl.Enable();
        
        
        _inputControl.Player.Aiming.performed += context =>
        {
            isAimingState = !isAimingState;

            if (isAimingState)
            {
                _moveCameraObject.SetActive(false);
            }
            else
            {
                _moveCameraObject.SetActive(true);
            }
        };
    }

    public override void OnDestroy()
    {
        _inputControl.Disable();
    }
}