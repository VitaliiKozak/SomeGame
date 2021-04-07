using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHaight;
    [SerializeField] private float _mass;
    [SerializeField] private float _graviry = -9.8f;
    private Vector3 _velosity;

    [SerializeField] private Transform _graundCheck;
    [SerializeField] private float _graundeDistance;
    [SerializeField] private LayerMask _graundMask;
    private bool _isGraunded;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        _isGraunded = Physics.CheckSphere(_graundCheck.position, _graundeDistance, _graundMask);

        if(_isGraunded && _velosity.y < 0)
        {
            _velosity.y = -2f;
        }

        var X = Input.GetAxis("Horizontal");
        var Z = Input.GetAxis("Vertical");
        var move = transform.right * X + transform.forward * Z;

        _characterController.Move(move * _speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && _isGraunded)
        {
            _velosity.y = Mathf.Sqrt(_jumpHaight * -1f * (_graviry * _mass));
        }

        _velosity.y +=  (_mass*_graviry) * Time.deltaTime;
        _characterController.Move(_velosity * Time.deltaTime);
    }

}
