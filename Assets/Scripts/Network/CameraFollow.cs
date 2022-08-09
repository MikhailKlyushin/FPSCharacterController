using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class CameraFollow : NetworkBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            _camera.Follow = transform;
            Instantiate(_camera);
        }
        else
        {
            Destroy(this);
        }
    }
}
