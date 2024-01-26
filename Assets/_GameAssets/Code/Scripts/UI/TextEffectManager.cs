using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;

public class TextEffectManager : MonoBehaviour
{
    public enum AnimStyle
    {
        IntoTheScreen,
    }

    static private Transform textParent;
    static private TextEffectManager _instance;
    static public TextEffectManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("TextSplashManager");
                _instance = obj.AddComponent(typeof(TextEffectManager)) as TextEffectManager;
                DontDestroyOnLoad(obj);
                textParent = GameObject.Find("TextEffectsParent").transform;
            }
            return _instance;
        }
    }

    public GameObject Load(string id)
    {
        GameObject prefab = Resources.Load<GameObject>("TextEffects/" + id);
        if (prefab == null)
            return null;
        GameObject obj = Instantiate(prefab, textParent);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.localEulerAngles = Vector3.zero;
        return obj;
    }

    public GameObject CreateText(string id, string text, AnimStyle animStyle = AnimStyle.IntoTheScreen)
    {
        GameObject obj = Load(id);
        if (id == null)
            return null;
        TextMeshProUGUI textObj = obj.GetComponentInChildren<TextMeshProUGUI>();
        if (textObj != null)
            textObj.text = text;
        
        if (animStyle == AnimStyle.IntoTheScreen)
        {
            textObj.CrossFadeAlpha(0f, 0f, false);
            textObj.CrossFadeAlpha(1f, 0.2f, false);
            obj.transform.localScale = Vector3.one * 3f;
            obj.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBounce);

            obj.transform.DOScale(0f, 0.2f).SetEase(Ease.InCubic).SetDelay(2f).OnComplete(()=>
            {
                Destroy(obj);
            });
        }

        return obj;
    }
}
