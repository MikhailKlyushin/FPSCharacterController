using Unity.Netcode;
using UnityEngine;

public class CharacterNetworkParams : NetworkBehaviour
{
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<Quaternion> Rotation = new NetworkVariable<Quaternion>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


    public NetworkVariable<Vector3> Velocity = new NetworkVariable<Vector3>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    
    public NetworkVariable<float> DirectionHorizontal = new NetworkVariable<float>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> DirectionVertical = new NetworkVariable<float>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    
    public NetworkVariable<short> Health = new NetworkVariable<short>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    
    public NetworkVariable<short> ActualBullets = new NetworkVariable<short>(default, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
}
