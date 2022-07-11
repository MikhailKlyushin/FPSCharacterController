using UnityEngine;
public struct SignalInputProvider : ISignalInput
{
    public Vector3 PositionToMove { get; set; }
    public Vector3 PositionToRotate { get; set; }
}