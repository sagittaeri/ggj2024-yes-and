using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        CollideTag(collision.gameObject.tag);
    }

    void OnTriggerEnter(Collider collider)
    {
        CollideTag(collider.gameObject.tag);
    }

    void CollideTag(string tagName)
    {
        if (tagName == "Agent" || tagName == "Untagged")
            return;
        Debug.Log("HIT COLLIDER: " + tagName);
        if (tagName == "Ground")
        {
            TextEffectManager.instance.CreateEffect(TextEffectManager.AnimStyle.Thump);
        }
    }
}
