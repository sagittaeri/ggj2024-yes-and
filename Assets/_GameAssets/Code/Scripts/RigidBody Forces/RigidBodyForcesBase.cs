using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RigidBodyForcesBase : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;
    [SerializeField] private ForceMode _forceMode;
    [SerializeField] private float _speed;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Agent"))
        {
            other.GetComponent<Rigidbody>().AddForce(_direction.normalized * _speed,_forceMode);
        }
    }
}
