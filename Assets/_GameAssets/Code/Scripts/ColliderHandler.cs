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
            AudioManager.instance.PlaySFX("hit");
        }
        if (tagName == "Woosh")
        {
            TextEffectManager.instance.CreateEffect(TextEffectManager.AnimStyle.Whoosh);
            AudioManager.instance.PlaySFX("paper");
        }
        if (tagName == "Sizzle")
        {
            TextEffectManager.instance.CreateEffect(TextEffectManager.AnimStyle.Sizzle);
            AudioManager.instance.PlaySFX("sizzle2");
        }
        
        if (tagName == "Thump")
        {
            TextEffectManager.instance.CreateEffect(TextEffectManager.AnimStyle.Thump);
            AudioManager.instance.PlaySFX("thump");
        }
        if (tagName == "Splat")
        {
            TextEffectManager.instance.CreateEffect(TextEffectManager.AnimStyle.Splat);
            AudioManager.instance.PlaySFX("land normal");
        }
        if (tagName == "Vroom")
        {
            TextEffectManager.instance.CreateEffect(TextEffectManager.AnimStyle.Vroom);
            AudioManager.instance.PlaySFX("jet");
        }
        if (tagName == "Bang")
        {
            TextEffectManager.instance.CreateEffect(TextEffectManager.AnimStyle.Bang);
            AudioManager.instance.PlaySFX("launch 2");
        }
        RandomGasp();
        RandomLaughter();
    }

    void RandomGasp()
    {
        if (Random.Range(0f, 1f) < 0.1f)
        {
            AudioManager.instance.PlaySFX("gasp");
        }
    }

    void RandomLaughter()
    {
        if (Random.Range(0f, 1f) < 0.2f)
        {
            AudioManager.instance.PlaySFX("laughter");
        }
    }
}
