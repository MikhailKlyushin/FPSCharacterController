using Unity.Netcode;
using UnityEngine;

public struct CharacterMovementState : INetworkSerializable
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Velocity;
    public Quaternion Rotate;
    
    public float SpeedAnimation;
    public float DirectionHorizontal;
    public float DirectionVertical;

    public void SetState(CharacterMovementState newState)
    {
        this.Position = newState.Position;
        this.Rotation = newState.Rotation;
        this.Velocity = newState.Velocity;
        this.Rotate = newState.Rotate;
        
        this.SpeedAnimation = newState.SpeedAnimation;
        this.DirectionHorizontal = newState.DirectionHorizontal;
        this.DirectionVertical = newState.DirectionVertical;
    }
    
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        if (serializer.IsReader)
        {
            var reader = serializer.GetFastBufferReader();
            reader.ReadValueSafe(out Position);
            reader.ReadValueSafe(out Rotation);
            reader.ReadValueSafe(out Velocity);
            reader.ReadValueSafe(out Rotate);
            
            reader.ReadValueSafe(out SpeedAnimation);
            reader.ReadValueSafe(out DirectionHorizontal);
            reader.ReadValueSafe(out DirectionVertical);
        }
        else
        {
            var writer = serializer.GetFastBufferWriter();
            writer.WriteValueSafe(Position);
            writer.WriteValueSafe(Rotation);
            writer.WriteValueSafe(Velocity);
            writer.WriteValueSafe(Rotate);
            
            writer.WriteValueSafe(SpeedAnimation);
            writer.WriteValueSafe(DirectionHorizontal);
            writer.WriteValueSafe(DirectionVertical);
        }
    }
}