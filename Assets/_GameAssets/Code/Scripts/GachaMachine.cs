using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class GachaMachine : MonoBehaviour
{
    public GameObject[] floors;
    public GachaCapsuleRandomiser gacha;
    public Transform mouth;
    public float vibrateAmplitude = 0.03f;
    public float vibrateDuration = 0.1f;
    public Transform openPos;

    static public GachaMachine instance;

    private Vector3 startPos;
    private Vector3 startAngles;

    void Awake()
    {
        instance = this;
        startPos = gacha.enclosure.localPosition;
        startAngles = gacha.enclosure.localEulerAngles;
        gacha.gameObject.SetActive(false);
        gacha.doContentAnimate = false;
    }

    [Button]
    public void StartVibrating()
    {
        StopVibrating();
        foreach (GameObject floor in floors)
        {
            floor.transform.DOLocalMoveY(vibrateAmplitude, vibrateDuration)
                .SetEase(Ease.InOutSine)
                .SetDelay(Random.Range(0f, 0.5f))
                .SetLoops(-1, LoopType.Yoyo);
        }
    }

    [Button]
    public void StopVibrating()
    {
        foreach (GameObject floor in floors)
        {
            floor.transform.DOKill();
            floor.transform.localPosition = Vector3.zero;
        }
    }

    [Button]
    public void RandomiseGacha()
    {
        gacha.enclosure.transform.DOKill();
        gacha.effectRoot.DOKill();
        gacha.gameObject.SetActive(false);
        gacha.breakRoot.gameObject.SetActive(false);
        gacha.doContentAnimate = false;

        string id = gacha.Randomise();
        StartVibrating();
        DOVirtual.DelayedCall(2f, ()=>
        {
            StopVibrating();
            gacha.gameObject.SetActive(true);
            gacha.enclosure.transform.position = mouth.position;
            gacha.enclosure.transform.rotation = Random.rotation;
            gacha.enclosure.transform.localScale = Vector3.one * 0.5f;
            gacha.enclosure.transform.DOLocalRotate(startAngles, 0.6f, RotateMode.FastBeyond360);
            gacha.enclosure.transform.DOLocalMoveX(startPos.x, 0.5f).SetEase(Ease.OutQuad);
            gacha.enclosure.transform.DOScale(1f, 0.5f).SetEase(Ease.OutQuad).SetDelay(0.2f);
            gacha.enclosure.transform.DOLocalMoveY(startPos.y, 0.5f).SetEase(Ease.OutSine).SetDelay(0.2f);
            gacha.enclosure.transform.DOLocalMoveZ(startPos.z, 0.55f).SetEase(Ease.OutSine).SetDelay(0.2f).OnComplete(()=>
            {
                gacha.BreakOpen();
                DOVirtual.DelayedCall(0.3f, ()=>
                {
                    gacha.nameText.text = id;
                    gacha.breakRoot.gameObject.SetActive(true);
                    gacha.effectRoot.localScale = Vector3.zero;
                    gacha.effectRoot.DOScale(1f, 0.3f).SetEase(Ease.OutQuad);

                    gacha.content.transform.DOKill();
                    gacha.content.transform.DOMove(openPos.position, 0.2f).SetEase(Ease.OutSine);
                    gacha.content.transform.DOScale(openPos.localScale, 0.2f).SetEase(Ease.OutSine);
                    gacha.content.transform.DOLocalRotate(openPos.localEulerAngles, 0.2f).SetEase(Ease.OutSine).OnComplete(()=>
                    {
                        gacha.doContentAnimate = true;
                    });
                });
            });
        });
    }
}
