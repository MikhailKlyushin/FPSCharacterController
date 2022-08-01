using UnityEngine;

public struct CharacterState
{
    public Vector3 Position;
    public Vector3 Velocity;
    public Quaternion Rotate;
    
    public float SpeedAnimation;
    public float DirectionHorizontal;
    public float DirectionVertical;

    public void SetState(CharacterState newState)
    {
        this.Position = newState.Position;
        this.Velocity = newState.Velocity;
        this.Rotate = newState.Rotate;
        
        this.SpeedAnimation = newState.SpeedAnimation;
        this.DirectionHorizontal = newState.DirectionHorizontal;
        this.DirectionVertical = newState.DirectionVertical;
    }
}