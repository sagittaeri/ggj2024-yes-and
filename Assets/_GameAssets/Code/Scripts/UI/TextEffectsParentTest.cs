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
    public GameObject CreateSplatText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            text = "TEST!!";
        return TextEffectManager.instance.CreateText("SplatTextEffect", text, TextEffectManager.AnimStyle.Splat);
    }
}
