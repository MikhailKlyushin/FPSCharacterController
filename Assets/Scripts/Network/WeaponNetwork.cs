using System;
using System.Collections;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class WeaponNetwork : MonoBehaviour
{

    private int _bulletsTotal = 30;
    private int _bulletsActual;

    private InputControl _inputControl;
    private bool onClickShoot = false;
    private bool isShoot = false;

    private float _timeNextShoot;
    private float _timeOneShoot = 0.1f;

    private void Start()
    {
        _inputControl = new InputControl();
        _inputControl.Enable();

        _bulletsActual = _bulletsTotal;


        _inputControl.Player.Attack.started += context =>
        {
            onClickShoot = true;
        };
        _inputControl.Player.Attack.canceled += context => { onClickShoot = false; };
        
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (onClickShoot)
            {
                if (!isShoot)
                {
                    isShoot = true;
                    _timeNextShoot = Time.time + _timeOneShoot;
                    Shoot();
                }
                else if (isShoot && (Time.time >= _timeNextShoot))
                {
                    _timeNextShoot = Time.time + _timeOneShoot;
                    Shoot();
                }
            }

        }).AddTo(transform);
    }

    private void Shoot()
    {
        Debug.Log("Shooted!!!!!!!!!!" + Time.time);
    }
    
}
