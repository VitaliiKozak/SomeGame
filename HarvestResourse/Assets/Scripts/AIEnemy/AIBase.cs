using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class AIBase : MonoBehaviour
{
    protected CharacterController _characterController;

    [SerializeField] protected float _speed;
    [SerializeField] protected float _graviry = -9.8f;
    protected Vector3 _velosity;

    public abstract void Start();
    
       
    

    protected Transform FindTarget(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag.Equals("Player")) return hitCollider.transform;
        }

        return default;
    }
}
