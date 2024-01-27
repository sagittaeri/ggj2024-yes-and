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
        Default,
        Sizzle,
        Vroom,
        Boing,
        Splat,
        Bang,
        Thump,
        Whoosh,
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

    public GameObject CreateText(string id, string text = null, AnimStyle animStyle = AnimStyle.Default)
    {
        GameObject obj = Load(id);
        if (id == null)
            return null;
        TextMeshProUGUI textObj = obj.GetComponentInChildren<TextMeshProUGUI>();
        if (!string.IsNullOrWhiteSpace(text))
        {
            if (textObj != null)
                textObj.text = text;
        }
        
        if (animStyle == AnimStyle.Default)
        {
            if (textObj != null)
            {
                textObj.CrossFadeAlpha(0f, 0f, false);
                textObj.CrossFadeAlpha(1f, 0.2f, false);
            }
            obj.transform.localScale = Vector3.one * 3f;
            obj.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBounce);

            obj.transform.DOScale(0f, 0.2f).SetEase(Ease.InCubic).SetDelay(2f).OnComplete(()=>
            {
                Destroy(obj);
            });
        }
        else if (animStyle == AnimStyle.Sizzle)
        {
            if (textObj != null)
            {
                textObj.CrossFadeAlpha(0f, 0f, false);
                textObj.CrossFadeAlpha(1f, 0.2f, false);
                textObj.transform.localScale = Vector3.one * 3f;
                textObj.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBounce);
            }

            Transform splatRoot = obj.transform.Find("ImageRoot");
            if (splatRoot != null)
            {
                foreach (Transform child in splatRoot)
                {
                    float delay = Random.Range(0f, 0.3f);
                    child.localScale = Vector3.zero;
                    child.DOScale(1f, 0.2f).SetDelay(delay);
                }
            }
            obj.transform.DOScale(0f, 0.2f).SetEase(Ease.InCubic).SetDelay(1.5f).OnComplete(()=>
            {
                Destroy(obj);
            });
        }
        else if (animStyle == AnimStyle.Splat)
        {
            if (textObj != null)
            {
                textObj.CrossFadeAlpha(0f, 0f, false);
                textObj.CrossFadeAlpha(1f, 0.2f, false);
                textObj.transform.localScale = Vector3.one * 3f;
                textObj.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBounce);
            }

            Transform splatRoot = obj.transform.Find("SplatRoot");
            if (splatRoot != null)
            {
                foreach (Transform child in splatRoot)
                {
                    float delay = Random.Range(0f, 0.3f);
                    child.localScale = Vector3.zero;
                    child.DOScale(1f, 0.2f).SetDelay(delay);
                }
            }
            obj.transform.DOScale(0f, 0.2f).SetEase(Ease.InCubic).SetDelay(1.5f).OnComplete(()=>
            {
                Destroy(obj);
            });
        }
        return obj;
    }

    public GameObject CreateSplatText(string text = null)
    {
        return CreateText("SplatTextEffect", text, AnimStyle.Splat);
    }

    public GameObject CreateEffect(AnimStyle animStyle)
    {
        return CreateText(animStyle + "TextEffect", null, animStyle);
    }
}
