using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

[RequireComponent(typeof(CharacterController))]
public class AIMove : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField] private float _speed;
    [SerializeField] private float _graviry = -9.8f;
    private Vector3 _velosity;

    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _targetTemp;
    [Range(1,100)]
    [SerializeField] private float _range = 5;
    private float _timeToUpdate = 1f;
    

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _targetTemp = GetRandomPositionToMove(transform.position);
    }

    private void FixedUpdate()
    {
        _velosity.y += _graviry * Time.deltaTime;
        _characterController.Move(_velosity * Time.deltaTime);

        _target = FindTarget(transform.position, _range);

        Vector3 move;
        if (_target != default)
        {
             move = _target.position - transform.position;
            _characterController.Move(move.normalized * _speed * Time.deltaTime);
        }
        else
        {
            if (_timeToUpdate < 0)
            {
                _targetTemp = GetRandomPositionToMove(transform.position);
                _timeToUpdate = Random.Range(3, 7);
            }
            move = _targetTemp - transform.position;
            _characterController.Move(move.normalized * (_speed / 2) * Time.deltaTime);
        }
        _timeToUpdate -= Time.deltaTime;
    }


    private Vector3 GetRandomPositionToMove(Vector3 defaulte)
    {
        Vector3 newVectror;

        newVectror.x = defaulte.x + Random.Range(-40, 40);
        newVectror.z = defaulte.z + Random.Range(-40, 40); 
        newVectror.y = defaulte.y + Random.Range(-15, 15);

        return newVectror;
    }

    private Transform FindTarget(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag.Equals("Player")) return hitCollider.transform;
        }

        return default;
    }
}
