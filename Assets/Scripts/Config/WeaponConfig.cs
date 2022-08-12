using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configuration Script/Weapon Config", order = 4)]
public class WeaponConfig : ScriptableObject
{
    [SerializeField] private short _weaponDamage = 25;
    [SerializeField] private short _bulletsTotal = 30;
    [SerializeField] private short _bulletsPerMinutes = 600;
    [SerializeField] private float _reloadingTime = 1f;

    public short WeaponDamage => _weaponDamage;
    public short BulletsTotal => _bulletsTotal;
    public short BulletsPerMinutes => _bulletsPerMinutes;
    public float ReloadingTime => _reloadingTime;
    
    public float TimeOneShoot
    {
        get { return 1f / (_bulletsPerMinutes / 60f); }
    }
}
