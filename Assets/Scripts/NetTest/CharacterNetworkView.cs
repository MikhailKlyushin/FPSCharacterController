using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterNetworkView : NetworkBehaviour
{
    [SerializeField] private float Smooth = 0.1f;
    
    private readonly NetworkVariable<PlayerNetworkData> _netState = new NetworkVariable<PlayerNetworkData>(writePerm: NetworkVariableWritePermission.Owner);
    private Vector3 _velocity;
    private float _rotationVelocity;
    private void Update()
    {
        if (IsOwner)
        {
            _netState.Value = new PlayerNetworkData()
            {
                Position = transform.position,
                Rotation = transform.rotation.eulerAngles
            };
        }
        else
        {
            transform.position =
                Vector3.SmoothDamp(transform.position, _netState.Value.Position, ref _velocity, Smooth);
            transform.rotation = Quaternion.Euler(
                0,
                Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, _netState.Value.Rotation.y, ref _rotationVelocity, Smooth),
                0);
        }
    }

    struct PlayerNetworkData : INetworkSerializable
    {
        private float _positionX;
        private float _positionZ;
        private short _rotationY;

        internal Vector3 Position
        {
            get => new Vector3(_positionX, 0, _positionZ);
            set
            {
                _positionX = value.x;
                _positionZ = value.z;
            }
        }

        internal Vector3 Rotation
        {
            get => new Vector3(0, _rotationY, 0);
            set => _rotationY = (short)value.y;
        }
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _positionX);
            serializer.SerializeValue(ref _positionZ);
            
            serializer.SerializeValue(ref _rotationY);
        }
    }
}
