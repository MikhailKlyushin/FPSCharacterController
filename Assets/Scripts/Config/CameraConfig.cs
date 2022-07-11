using UnityEngine;

[CreateAssetMenu(fileName = "CameraConfig", menuName = "Configuration Script/Camera Config", order = 1)]
public class CameraConfig : ScriptableObject
{
    [SerializeField] private string _pathToPrefab = "Prefabs/ThirdPersonCamera";
    
    [SerializeField] private float _sensivityHorisontal = 20f;
    [SerializeField] private float _sensivityVertical = 20f;

    [SerializeField] private float _minimumVerticalAngle = -45f;
    [SerializeField] private float _maximumVerticalAngle = 45f;
    
    [SerializeField] private float _smoothSpeed = 0.15f;


    public string PathToPrefab => _pathToPrefab;
    public float SensivityHorizontal => _sensivityHorisontal;
    public float SensivityVertical => _sensivityVertical;
    public float MinimumVerticalAngle => _minimumVerticalAngle;
    public float MaximumVerticalAngle => _maximumVerticalAngle;
    public float SmoothSpeed => _smoothSpeed;
}
