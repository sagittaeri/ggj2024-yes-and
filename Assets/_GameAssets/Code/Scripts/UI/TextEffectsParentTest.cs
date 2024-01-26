using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TextEffectsParentTest : MonoBehaviour
{
    [Button]
    public GameObject CreateText(string id, string text)
    {
        if (id == null)
            id = "DefaultTextEffect";
        return TextEffectManager.instance.CreateText(id, text);
    }
}
