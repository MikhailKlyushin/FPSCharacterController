using UnityEngine;


[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configuration Script/Character Config", order = 1)]

public class CharacterConfig : ScriptableObject
{
    #region [SerializeField]

    [SerializeField] private float _moveSpeed = 3;

    [SerializeField] private float _sensivityHorisontal = 20f;
    [SerializeField] private float _sensivityVertical = 20f;

    [SerializeField] private float _minimumVerticalAngle = -45f;
    [SerializeField] private float _maximumVerticalAngle = 45f;

    #endregion


    public float MoveSpeed => _moveSpeed;
    public float SensivityHorizontal => _sensivityHorisontal;
    
    //TODO: All of properties below are not using
    public float SensivityVertical => _sensivityVertical;
    public float MinimumVerticalAngle => _minimumVerticalAngle;
    public float MaximumVerticalAngle => _maximumVerticalAngle;

}
