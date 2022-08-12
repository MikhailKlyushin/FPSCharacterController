using UniRx;
using Unity.Netcode;
using UnityEngine;

public class WeaponNetwork : NetworkBehaviour
{
    [SerializeField] private Transform _raycastPoint;
    
    private short _bulletsTotal = 30;
    private short _bulletsActual;
    private short _weaponDamage = 25;
    

    private InputControl _inputControl;
    private bool _onClickShoot = false;

    private float _timeNextShoot = 0f;
    private float _timeOneShoot = 0.1f;

    private void Start()
    {
        if (IsOwner)
        {
            _inputControl = new InputControl();
            _inputControl.Enable();

            _bulletsActual = _bulletsTotal;


            _inputControl.Player.Attack.started += context => { _onClickShoot = true; };
            _inputControl.Player.Attack.canceled += context => { _onClickShoot = false; };

            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (_onClickShoot && (Time.time >= _timeNextShoot))
                {
                    _timeNextShoot = Time.time + _timeOneShoot;
                    Shoot();
                }

            }).AddTo(transform);
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(_raycastPoint.position, _raycastPoint.forward, out hit))
        {
            //Debug.DrawRay(_raycastPoint.position, _raycastPoint.forward * hit.distance, Color.green);
            GameObject hitObject = hit.transform.gameObject;
            CharacterNetworkHealth health = hitObject.GetComponent<CharacterNetworkHealth>();

            if (health != null)
            {
                health.Remove(_weaponDamage);
                Debug.Log(health.gameObject);
            }
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(_raycastPoint.position, _raycastPoint.forward * 1000, Color.red);
            Debug.Log("Did not Hit");
        }
    }
}
