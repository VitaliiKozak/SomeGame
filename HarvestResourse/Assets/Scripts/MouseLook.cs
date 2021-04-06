using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float _mouseSensetivity;
    [SerializeField] private Transform _playerBody;

    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = 40;
    }

    private void FixedUpdate()
    {
        var mouseX = Input.GetAxis("Mouse X") * _mouseSensetivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * _mouseSensetivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        _playerBody.Rotate(Vector3.up * mouseX);
    }
}
