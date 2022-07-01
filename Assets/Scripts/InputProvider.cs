using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InputProvider
{
    private float _horizontalPosition;
    private float _verticalPosition;

    private Vector3 _positionToMove;

    public delegate void InputHandler(Vector3 position);

    public event InputHandler Notify;


    async public void Update()
    {
        while (true)
        {
            _horizontalPosition = Input.GetAxis("Horizontal");
            _verticalPosition = Input.GetAxis("Vertical");


            if ((_horizontalPosition >= 0.2f) || (_horizontalPosition <= 0.2) || (_verticalPosition >= 0.2f) ||
                (_verticalPosition <= 0.2f))
            {
                _positionToMove = new Vector3(_horizontalPosition, 0, _verticalPosition);
                Notify?.Invoke(_positionToMove);
            }
            else
            {
                _positionToMove = new Vector3(0, 0, 0);
                Notify?.Invoke(_positionToMove);
            }

            await Task.Delay(100);
            Debug.Log("Process active");
        }
    }
}
