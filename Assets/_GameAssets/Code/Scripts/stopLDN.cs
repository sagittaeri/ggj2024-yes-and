using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopLDN : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Agent"))
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
