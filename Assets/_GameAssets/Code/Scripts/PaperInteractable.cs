using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperInteractable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Agent"))
        {
            other.GetComponent<Rigidbody>().velocity = other.GetComponent<Rigidbody>().velocity / 2;
        }
    }
}
