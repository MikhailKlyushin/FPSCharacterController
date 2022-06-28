using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WatchCamera : MonoBehaviour
{
    [SerializeField] [Range(1f, 5f)] private float _angularSpeed = 1f;

    [SerializeField] private Transform _target;

    private float _angleY;

    void Start()
    {
        _angleY = transform.rotation.y;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) _angleY -= _angularSpeed;
        if (Input.GetKey(KeyCode.S)) _angleY += _angularSpeed;

        transform.position = _target.transform.position;
        transform.rotation = Quaternion.Euler(0, _angleY, 0);
    }
}
