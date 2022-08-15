using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    //TODO: Rewrite! 
    [SerializeField] private Transform _transformToFollow;
    
    private VisualElement _healthBar;
    private VisualElement _healthBarProgress;
    private Label _healthBarPercent;
    
    private Camera _camera;
    private CharacterNetworkParams _state;
    
    
    private void Start()
    {
        _camera = Camera.main;
        
        _healthBar = GetComponent<UIDocument>().rootVisualElement.Q("Container");
        _healthBarProgress = _healthBar.Q<VisualElement>("HealthBarProgress");
        _healthBarPercent = _healthBar.Q<Label>("PercentLabel");

        Debug.Log(" _healthBarProgress.style.width = " + _healthBarProgress.style.maxWidth);

        _state = GetComponent<CharacterNetworkParams>();
    }
    
    private void LateUpdate()
    {
        if (_transformToFollow != null)
        {
            SetPosition();
            SetHealthBarPercent(_state.Health.Value);
        }
    }

    private void SetPosition()
    {
        Vector2 newPosition = RuntimePanelUtils.CameraTransformWorldToPanel (
            _healthBar.panel, _transformToFollow.position, _camera);
        
        newPosition = new Vector3(newPosition.x - _healthBar.layout.width / 2, newPosition.y, 0f);
        _healthBar.transform.position = new Vector3(newPosition.x, newPosition.y, 0);
        //_healthBar.transform.position = Vector3.Lerp(_healthBar.transform.position, newPosition, 0.3f);

    }
    
    private void SetHealthBarPercent(short value)
    {
        _healthBarProgress.style.width = Length.Percent(value);
        _healthBarPercent.text = $"{value}%";
    }
}
