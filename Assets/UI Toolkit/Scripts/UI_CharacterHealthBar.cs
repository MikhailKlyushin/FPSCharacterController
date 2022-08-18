using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_CharacterHealthBar : NetworkBehaviour
{
    [SerializeField] private GameObject _characterGameObject;
    
    private VisualElement _healthBar;
    private VisualElement _healthBarProgress;
    private Label _healthBarPercent;
    
    private Camera _camera;
    private CharacterNetworkParams _state;
    
    
    private void Start()
    {
        if (IsOwner)
        {
            GetComponent<UIDocument>().enabled = false;
            this.enabled = false;
        }
        
        _camera = Camera.main;
        
        _healthBar = GetComponent<UIDocument>().rootVisualElement.Q("Container");
        _healthBarProgress = _healthBar.Q<VisualElement>("HealthBarProgress");
        _healthBarPercent = _healthBar.Q<Label>("PercentLabel");

        Debug.Log(" _healthBarProgress.style.width = " + _healthBarProgress.style.maxWidth);

        //todo: question
        //_state = _characterGameObject.GetComponent<CharacterNetworkParams>();
        _state = GetComponentInParent<CharacterNetworkParams>();
    }

    private void LateUpdate()
    {

        SetPosition();
        SetHealthBarPercent(_state.Health.Value);
    }

    private void SetPosition()
    {
        Vector2 newPosition = RuntimePanelUtils.CameraTransformWorldToPanel (
            _healthBar.panel, this.transform.position, _camera);
        
        newPosition = new Vector3(newPosition.x - _healthBar.layout.width / 2, newPosition.y, 0f);
        _healthBar.transform.position = new Vector3(newPosition.x, newPosition.y, 0);

    }
    
    private void SetHealthBarPercent(short value)
    {
        _healthBarProgress.style.width = Length.Percent(value);
        _healthBarPercent.text = $"{value}%";
    }
}
