using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AIType {Wander, Patrol, Warior};

[RequireComponent(typeof(CharacterController))]
public class AIMove : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField] private float _speed;
    [SerializeField] private float _graviry = -9.8f;
    private Vector3 _velosity;
    
    [SerializeField] private AIType _aIType;

    [Header("Defoulte AI")]
    [SerializeField] private Transform _target;
    [Range(1, 100)]
    [SerializeField] private float _range = 5;

    [Header("Wander SetUp")]
    [SerializeField] private Vector3 _targetTemp;
    private float _timeToUpdate = 1f;


    [Header("Warior SetUp")]
    [SerializeField] private Vector3 _spawnPosition;
    [SerializeField] private bool _updateSpawnPosition = false;

    [Header("Patrol SetUp")]
    [SerializeField] private List<Transform> _patrolPoints;
    private Transform _currentPoint;
    private int _curentPointNumber = 0;

  
    

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _targetTemp = GetRandomPositionToMove(transform.position);
        _target = null;

        if(_updateSpawnPosition)
        {
            _spawnPosition = transform.position;
        }

        if(_aIType == AIType.Patrol && _patrolPoints.Count <= 0)
        {
            throw new Exception("List of patrol points is Empty!!!");
        }
        else if(_aIType == AIType.Patrol)
        {
            _currentPoint = _patrolPoints[_curentPointNumber];
        }
    }

    private void Update()
    {
        _velosity.y += _graviry * Time.deltaTime;
        _characterController.Move(_velosity * Time.deltaTime);

        switch (_aIType)
        {
            case AIType.Wander:
                {
                    _target = FindTarget(transform.position, _range);
                    Vector3 move;
                    if (_target != default)
                    {
                        move = _target.position - transform.position;
                        _characterController.Move(move.normalized * _speed * Time.deltaTime);

                        if (Vector3.Distance(transform.position, _target.position) > _range)
                        {
                            _target = null;
                        }
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
                break;

            case AIType.Patrol:
                {
                    if(_currentPoint == null)
                    {
                        if(_patrolPoints.Count - 1 == _curentPointNumber)
                        {
                            _curentPointNumber = 0;
                            _currentPoint = _patrolPoints[_curentPointNumber];
                        }
                        else
                        {
                            _currentPoint = _patrolPoints[++_curentPointNumber];
                        }
                    }
                    else
                    {
                        var move = _currentPoint.position - transform.position;
                        _characterController.Move(move.normalized * _speed * Time.deltaTime);

                        if(Vector3.Distance(transform.position, _currentPoint.position) < 1f)
                        {
                            _currentPoint = null;
                        }

                        _target = FindTarget(transform.position, _range);
                        
                        if(_target != null)
                        {
                            _currentPoint = _target;
                        }
                    }
                }

                break;
            case AIType.Warior:
                {
                    if(_target == null || _target == default)
                    {
                        _target = FindTarget(transform.position, _range);
                    }
                    Vector3 move;
                    if (_target != default || _target != null)
                    {
                        move = _target.position - transform.position;
                        _characterController.Move(move.normalized * _speed * Time.deltaTime);

                        if (Vector3.Distance(transform.position, _target.position) > _range)
                        {
                            _target = null;
                        }
                    }
                    else
                    {
                        if(Vector3.Distance(transform.position, _spawnPosition) > 1)
                        {
                            move = _spawnPosition - transform.position;
                            _characterController.Move(move.normalized * _speed * Time.deltaTime);
                        }
                    }
                }
                break;
        }

        
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
