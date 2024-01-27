using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public Transform textParent;
    static private TextEffectManager _instance;
    static public TextEffectManager instance
    {
        get
        {
            // if (_instance == null)
            // {
            //     GameObject obj = new GameObject("TextSplashManager");
            //     _instance = obj.AddComponent(typeof(TextEffectManager)) as TextEffectManager;
            //     DontDestroyOnLoad(obj);
            //     textParent = GameObject.Find("TextEffectsParent").transform;
            // }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
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
                textObj.transform.DOScale(1.05f, 0.6f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            }

            Transform imageRoot = obj.transform.Find("ImageRoot");
            if (imageRoot != null)
            {
                imageRoot.DOShakePosition(2f, 10f);
            }
            obj.transform.DOScale(0f, 0.2f).SetEase(Ease.InCubic).SetDelay(1.5f).OnComplete(()=>
            {
                Destroy(obj);
            });
        }
        else if (animStyle == AnimStyle.Vroom)
        {
            if (textObj != null)
            {
                textObj.CrossFadeAlpha(0f, 0f, false);
                textObj.CrossFadeAlpha(1f, 0.2f, false);
                textObj.transform.DOScale(1.2f, 0.5f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo);
            }

            Transform imageRoot = obj.transform.Find("ImageRoot");
            if (imageRoot != null)
            {
                imageRoot.DOLocalMoveX(-50f, 0.5f);
                imageRoot.DOScaleX(0.9f, 0.5f).SetEase(Ease.OutSine).OnComplete(()=>
                {
                    imageRoot.DOScaleX(1f, 0.2f);
                    imageRoot.DOLocalMoveX(2000f, 1f).OnComplete(()=>
                    {
                        imageRoot.gameObject.SetActive(false);
                    });
                });
            }
            obj.transform.DOScale(0f, 0.2f).SetEase(Ease.InCubic).SetDelay(1.5f).OnComplete(()=>
            {
                Destroy(obj);
            });
        }
        else if (animStyle == AnimStyle.Boing)
        {
            if (textObj != null)
            {
                textObj.CrossFadeAlpha(0f, 0f, false);
                textObj.CrossFadeAlpha(1f, 0.2f, false);
                textObj.transform.DOScale(1.2f, 0.5f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo);
            }

            Transform imageRoot = obj.transform.Find("ImageRoot");
            if (imageRoot != null)
            {
                imageRoot.DOLocalMoveY(-50f, 0.5f);
                imageRoot.DOScaleY(0.9f, 0.5f).SetEase(Ease.OutSine).OnComplete(()=>
                {
                    imageRoot.DOScaleY(1f, 0.2f);
                    imageRoot.DOLocalMoveY(100f, 0.5f).SetEase(Ease.InOutSine).OnComplete(()=>
                    {
                        imageRoot.DOLocalMoveY(0f, 0.4f).SetEase(Ease.InQuad);
                        imageRoot.DOScaleY(0.8f, 0.2f).SetDelay(0.3f);
                    });
                });
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
                textObj.transform.DOScale(1f, 0.2f).SetEase(Ease.InQuad);
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
        else if (animStyle == AnimStyle.Bang)
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
        else if (animStyle == AnimStyle.Thump)
        {
            if (textObj != null)
            {
                textObj.CrossFadeAlpha(0f, 0f, false);
                textObj.CrossFadeAlpha(1f, 0.2f, false);
            }
            obj.transform.localScale = Vector3.one * 1.5f;
            obj.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBounce);

            obj.transform.DOScale(0f, 0.2f).SetEase(Ease.InCubic).SetDelay(2f).OnComplete(()=>
            {
                Destroy(obj);
            });
        }
        else if (animStyle == AnimStyle.Whoosh)
        {
            if (textObj != null)
            {
                textObj.CrossFadeAlpha(0f, 0f, false);
                textObj.CrossFadeAlpha(1f, 0.4f, false);
                textObj.transform.DOScale(1.2f, 1.5f).SetEase(Ease.Linear);
                DOVirtual.DelayedCall(1f, ()=>
                {
                    textObj.CrossFadeAlpha(0f, 0.4f, false);
                });
            }

            Transform imageRoot = obj.transform.Find("ImageRoot");
            Image image = imageRoot.GetComponentInChildren<Image>();
            if (imageRoot != null)
            {
                imageRoot.transform.DOScale(1.5f, 1.5f).SetEase(Ease.Linear);
                image.CrossFadeAlpha(0f, 0f, false);
                image.CrossFadeAlpha(1f, 0.4f, false);
                DOVirtual.DelayedCall(1f, ()=>
                {
                    image.CrossFadeAlpha(0f, 0.4f, false);
                });
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
