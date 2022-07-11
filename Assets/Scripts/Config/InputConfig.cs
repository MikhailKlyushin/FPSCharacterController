using UnityEngine;

[CreateAssetMenu(fileName = "InputConfig", menuName = "Configuration Script/Input Config", order = 3)]
public class InputConfig : ScriptableObject
{
    [SerializeField] private float _blindSpot = 0.2f;

    public float BlindSpot => _blindSpot;
}
