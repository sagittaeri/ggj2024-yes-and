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
        if (tagName == "Woosh")
        {
            TextEffectManager.instance.CreateEffect(TextEffectManager.AnimStyle.Whoosh);
        }
        if (tagName == "Sizzle")
        {
            TextEffectManager.instance.CreateEffect(TextEffectManager.AnimStyle.Sizzle);
        }
        
        if (tagName == "Thump")
        {
            TextEffectManager.instance.CreateEffect(TextEffectManager.AnimStyle.Thump);
        }
        if (tagName == "Splat")
        {
            TextEffectManager.instance.CreateEffect(TextEffectManager.AnimStyle.Splat);
        }
        if (tagName == "Vroom")
        {
            TextEffectManager.instance.CreateEffect(TextEffectManager.AnimStyle.Vroom);
        }
        if (tagName == "Bang")
        {
            TextEffectManager.instance.CreateEffect(TextEffectManager.AnimStyle.Bang);
        }
        
    }
}
