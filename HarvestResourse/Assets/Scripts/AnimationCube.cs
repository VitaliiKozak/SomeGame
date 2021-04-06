using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCube : MonoBehaviour
{
    [Header("Color cube")]
    [SerializeField] private Material _baseMaterial;
    [SerializeField] private Color _aColor;
    [SerializeField] private Color _bColor;
     private float _value;

    [Header("Move cube")]
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _trajectory;
    [SerializeField] private Transform _target;

    private void FixedUpdate()
    {
        _value = Mathf.Cos(Time.time * 3f) * 0.5f + 0.5f;

        _baseMaterial.color = Color.Lerp(_aColor, _bColor, _value);

        transform.RotateAround(_target.transform.position, _trajectory, _speed * Time.deltaTime);
    }
}
