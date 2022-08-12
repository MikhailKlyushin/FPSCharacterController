public interface IWeapon
{
    public WeaponConfig Config { get; }
    public short BulletsActual { get; }

    public void Shoot();
}
