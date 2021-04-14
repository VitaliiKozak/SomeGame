using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWarior : AIBase
{
    [SerializeField] private Transform _target;
    [Range(1, 100)]
    [SerializeField] private float _range = 5;
    [SerializeField] private Vector3 _spawnPosition;
    [SerializeField] private bool _updateSpawnPosition = false;

    public override void Start()
    {
        _characterController = GetComponent<CharacterController>();
        if (_updateSpawnPosition)
        {
            _spawnPosition = transform.position;
        }
    }

    private void Update()
    {
        _velosity.y += _graviry * Time.deltaTime;
        _characterController.Move(_velosity * Time.deltaTime);

        if (_target == null || _target == default)
        {
            _target = FindTarget(transform.position, _range);
        }
        Vector3 move;
        if (_target != null)
        {
            Debug.Log("move");
            move = _target.position - transform.position;
            _characterController.Move(move.normalized * _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _target.position) > _range)
            {
                _target = null;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, _spawnPosition) > 1)
            {
                move = _spawnPosition - transform.position;
                _characterController.Move(move.normalized * _speed * Time.deltaTime);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
