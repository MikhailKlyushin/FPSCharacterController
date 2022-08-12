using System.Collections;
using UniRx;
using Unity.Netcode;
using UnityEngine;

public class WeaponNetwork : MonoBehaviour
{
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private WeaponConfig _config;

    private InputControl _inputControl;
    
    private bool _onClickShoot = false;
    private float _timeNextShoot = 0f;

    private short _bulletsActual;


    private void Start()
    {
        if (NetworkManager.Singleton.IsClient)
        {
            _inputControl = new InputControl();
            _inputControl.Enable();

            _bulletsActual = _config.BulletsTotal;


            _inputControl.Player.Attack.started += context => { _onClickShoot = true; };
            _inputControl.Player.Attack.canceled += context => { _onClickShoot = false; };

            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (_onClickShoot && (Time.time >= _timeNextShoot))
                {
                    _timeNextShoot = Time.time + _config.TimeOneShoot;
                    Shoot();
                }

            }).AddTo(transform);
        }
    }

    private void Shoot()
    {
        if (_bulletsActual > 0)
        {
            _bulletsActual -= 1;

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(_raycastPoint.position, _raycastPoint.forward, out hit))
            {
                //Debug.DrawRay(_raycastPoint.position, _raycastPoint.forward * hit.distance, Color.green);
                GameObject hitObject = hit.transform.gameObject;
                CharacterNetworkHealth health = hitObject.GetComponent<CharacterNetworkHealth>();

                if (health != null)
                {
                    health.Remove(_config.WeaponDamage);
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
        else if (_bulletsActual == 0) 
        {
            StartCoroutine(ReloadingBullets(_config.ReloadingTime));
        }
        
        IEnumerator ReloadingBullets(float reloadTime)
        {
            yield return new WaitForSeconds(reloadTime);
            _bulletsActual = _config.BulletsTotal;
        }

        Debug.Log("Bullets = " + _bulletsActual);
    }
}
