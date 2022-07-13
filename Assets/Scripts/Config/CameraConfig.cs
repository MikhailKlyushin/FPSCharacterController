using UnityEngine;

[CreateAssetMenu(fileName = "CameraConfig", menuName = "Configuration Script/Camera Config", order = 1)]
public class CameraConfig : ScriptableObject
{
    [SerializeField] private string _pathToPrefab = "Prefabs/ThirdPersonCamera";
    
    [SerializeField] private float _sensitivityHorisontal = 20f;
    [SerializeField] private float _sensitivityVertical = 20f;

    [SerializeField] private float _minimumVerticalAngle = -45f;
    [SerializeField] private float _maximumVerticalAngle = 45f;
    
    [SerializeField] private float _smoothSpeed = 0.15f;


    public string PathToPrefab => _pathToPrefab;
    public float SensitivityHorizontal => _sensitivityHorisontal;
    public float SensitivityVertical => _sensitivityVertical;
    public float MinimumVerticalAngle => _minimumVerticalAngle;
    public float MaximumVerticalAngle => _maximumVerticalAngle;
    public float SmoothSpeed => _smoothSpeed;
}
