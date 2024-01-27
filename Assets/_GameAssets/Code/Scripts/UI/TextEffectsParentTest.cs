using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TextEffectsParentTest : MonoBehaviour
{
    [Button]
    public GameObject CreateText(string text, string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            id = "DefaultTextEffect";
        if (string.IsNullOrWhiteSpace(text))
            text = "TEST!!";
        return TextEffectManager.instance.CreateText(id, text);
    }

    [Button]
    public GameObject CreateEffect(TextEffectManager.AnimStyle animStyle = TextEffectManager.AnimStyle.Splat)
    {
        return TextEffectManager.instance.CreateEffect(animStyle);
    }
}
