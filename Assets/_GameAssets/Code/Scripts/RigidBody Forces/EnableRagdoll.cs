using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRagdoll : MonoBehaviour
{
    [SerializeField] private bool _ragDollActive = false;

    [SerializeField] private Rigidbody _rb => GetComponent<Rigidbody>();

    void Update()
    {
        if (_ragDollActive)
        {
            foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            {
                _ragDollActive = false;
                _rb.isKinematic = true;
                _rb.useGravity = false;
                
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        }
    }
}
