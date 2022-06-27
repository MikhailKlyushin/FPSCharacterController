using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes Axes = RotationAxes.MouseXAndY;

    public float sensivityHorisontal = 9f;
    public float sensivityVertical = 9f;

    public float minimumVertical = -45f;
    public float maximumVertical = 45f;

    private float _rotationX = 0;

    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
            rigidbody.freezeRotation = true;
    }


    void Update()
    {
        if (Axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensivityHorisontal, 0);
        }
        else if (Axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensivityVertical;
            _rotationX = Mathf.Clamp(_rotationX, minimumVertical, maximumVertical);

            float rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensivityVertical;
            _rotationX = Mathf.Clamp(_rotationX, minimumVertical, maximumVertical);

            float delta = Input.GetAxis("Mouse X") * sensivityHorisontal;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}
