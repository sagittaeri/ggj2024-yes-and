using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRagdoll : MonoBehaviour
{
    [SerializeField] private bool _ragDollActive = false;

    private Rigidbody _rb => GetComponent<Rigidbody>();
    [SerializeField] private float _launchStrength = 100f;
    [SerializeField] private Rigidbody _ragdollTorso;

    private void Awake()
    {
        if (!_ragdollTorso)
            Debug.LogError("Torso Ragdoll Unassigned.");
        
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            _ragDollActive = false;
            _rb.isKinematic = false;
            _rb.useGravity = true;
            
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    void Update()
    {
        if (_ragDollActive && _ragdollTorso)
        {
            foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            {
                _ragDollActive = false;
                _rb.isKinematic = true;
                _rb.useGravity = false;
                
                _ragdollTorso.AddForce((transform.forward + transform.up) * _launchStrength,ForceMode.VelocityChange);
                
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        }
    }
}
