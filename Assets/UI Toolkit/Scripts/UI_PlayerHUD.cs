using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;
using UIDocument = UnityEngine.UIElements.UIDocument;

public class UI_PlayerHUD : NetworkBehaviour
{
    [SerializeField] private WeaponNetwork _weapon;
    
    private VisualElement _healthBar;
    private VisualElement _healthBarProgress;
    private Label _healthBarPercent;

    private Label _actualBullet;
    
    private CharacterNetworkParams _state;


    private void Start()
    {
        if (!IsOwner)
        {
            GetComponent<UIDocument>().enabled = false;
            this.enabled = false;
        }
        
        _healthBar = GetComponent<UIDocument>().rootVisualElement.Q("Container");
        _healthBarProgress = _healthBar.Q<VisualElement>("HealthBarProgress");
        _healthBarPercent = _healthBar.Q<Label>("PercentLabel");
        
        _actualBullet = _healthBar.Q<Label>("ActualBulletLabel");
        
        _state = GetComponentInParent<CharacterNetworkParams>();
    }
    
    private void LateUpdate()
    {
        Debug.Log("net nealth" + _state.Health.Value);
        SetParams();
    }

    private void SetParams()
    {
        _healthBarProgress.style.width = Length.Percent(_state.Health.Value);
        _healthBarPercent.text = $"{_state.Health.Value}%";
        _actualBullet.text = $"{_weapon.BulletActual}/{_weapon.BulletTotal}";
        Debug.Log("health = " + _state.Health.Value);
    }
}
