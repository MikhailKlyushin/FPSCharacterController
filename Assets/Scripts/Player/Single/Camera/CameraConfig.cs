using UnityEngine;

[CreateAssetMenu(fileName = "CameraConfig", menuName = "Configuration Script/Camera Config", order = 1)]
public class CameraConfig : ScriptableObject
{
    [SerializeField] private GameObject _cameraPrefab;
    
    [SerializeField] private float _sensitivityHorisontal = 20f;
    [SerializeField] private float _sensitivityVertical = 20f;

    [SerializeField] private float _minimumVerticalAngle = -45f;
    [SerializeField] private float _maximumVerticalAngle = 45f;
    
    [SerializeField] private float _smoothSpeed = 0.15f;
    [SerializeField] private float _smoothRotate= 0.8f;


    public GameObject CameraPrefab => _cameraPrefab;
    public float SensitivityHorizontal => _sensitivityHorisontal;
    public float SensitivityVertical => _sensitivityVertical;
    public float MinimumVerticalAngle => _minimumVerticalAngle;
    public float MaximumVerticalAngle => _maximumVerticalAngle;
    public float SmoothSpeed => _smoothSpeed;
    public float SmoothRotate => _smoothRotate;
}
