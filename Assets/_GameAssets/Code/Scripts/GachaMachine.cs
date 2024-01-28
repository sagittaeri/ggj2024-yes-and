using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using DG.Tweening;
using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.UI;

public class GachaMachine : MonoBehaviour
{
    public enum State
    {
        BeforePull,
        Pulling,
        Reading,
        After,
    }

    [Serializable]
    public class Person
    {
        public string id;
        public FuckWits fuckwit;
        public string name;
        public string description;
        public Sprite paper;
    }
    public Person[] persons;
    
    public GameObject[] floors;
    public GachaCapsuleRandomiser gacha;
    public Transform mouth;
    public float vibrateAmplitude = 0.03f;
    public float vibrateDuration = 0.1f;
    public Transform openPos;
    public Button playButton;

    static public GachaMachine instance;

    [SerializeField] private Transform _knob;
    private Vector3 spinVector = Vector3.zero;
    private bool _spinKnob;
    private Vector3 startPos;
    private Vector3 startAngles;
    private Tween autoTween;
    private FuckWits currentFuckwit;
    public State state = State.BeforePull;

    void Awake()
    {
        instance = this;
        startPos = gacha.enclosure.localPosition;
        startAngles = gacha.enclosure.localEulerAngles;
        gacha.gameObject.SetActive(false);
        gacha.doContentAnimate = false;
    }

    void Update()
    {
        if (_spinKnob)
        {
            spinVector.z = Mathf.MoveTowardsAngle(spinVector.z, 180, Time.deltaTime*255);
        }

        _knob.localEulerAngles = spinVector;
        
        if (state == State.BeforePull && Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        else if (state == State.BeforePull && Input.GetKeyDown(KeyCode.Space))
            RandomiseGacha();
        else if (state == State.Reading && Input.anyKeyDown)
            ContinueToGame();
    }

    [Button]
    public void StartVibrating()
    {
        StopVibrating();
        foreach (GameObject floor in floors)
        {
            floor.transform.DOLocalMoveY(vibrateAmplitude, vibrateDuration)
                .SetEase(Ease.InOutSine)
                .SetDelay(UnityEngine.Random.Range(0f, 0.5f))
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

    public void Reset()
    {
        gacha.enclosure.transform.DOKill();
        gacha.effectRoot.DOKill();
        gacha.gameObject.SetActive(false);
        gacha.breakRoot.gameObject.SetActive(false);
        gacha.doContentAnimate = false;
        playButton.interactable = true;
    }

    [Button]
    public void RandomiseGacha()
    {
        _spinKnob = true;
        state = State.Pulling;
        gacha.enclosure.transform.DOKill();
        gacha.effectRoot.DOKill();
        gacha.gameObject.SetActive(false);
        gacha.breakRoot.gameObject.SetActive(false);
        gacha.doContentAnimate = false;
        playButton.interactable = false;
        _spinKnob = true;

        AudioManager.instance.PlaySFX("Gacha Sting");
        
        
        string id = gacha.Randomise();
        StartVibrating();
        DOVirtual.DelayedCall(2f, ()=>
        {
            StopVibrating();
            gacha.gameObject.SetActive(true);
            gacha.enclosure.transform.position = mouth.position;
            gacha.enclosure.transform.rotation = UnityEngine.Random.rotation;
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
                    foreach (Person p in persons)
                    {
                        if (p.id == id)
                        {
                            gacha.nameText.text = p.name;
                            gacha.descriptionText.text = p.description;
                            gacha.spriteRenderer.sprite = p.paper;
                            currentFuckwit = p.fuckwit;
                            break;
                        }
                    }
                    gacha.breakRoot.gameObject.SetActive(true);
                    gacha.effectRoot.localScale = Vector3.zero;
                    gacha.effectRoot.DOScale(1f, 0.3f).SetEase(Ease.OutQuad);

                    gacha.content.transform.DOKill();
                    gacha.content.transform.DOMove(openPos.position, 0.2f).SetEase(Ease.OutSine);
                    gacha.content.transform.DOScale(openPos.localScale, 0.2f).SetEase(Ease.OutSine);
                    gacha.content.transform.DOLocalRotate(openPos.localEulerAngles, 0.2f).SetEase(Ease.OutSine).OnComplete(()=>
                    {
                        gacha.doContentAnimate = true;
                        if (autoTween != null)
                            autoTween.Kill();
                        AudioManager.instance.PlayMusic("Tune 2", 0.5f);
                        state = State.Reading;
                        autoTween = DOVirtual.DelayedCall(20f, ()=>
                        {
                            ContinueToGame();
                        });
                    });
                });
            });
        });
    }

    public void ContinueToGame()
    {
        state = State.After;
        if (autoTween != null)
            autoTween.Kill();
        autoTween = null;
        DontDestroyOnLoad(gacha.breakRoot.gameObject);
        gacha.breakRoot.SetParent(null);
        Reset();
        gacha.breakRoot.gameObject.SetActive(true);
        SceneManager.LoadScene("GameLevel", LoadSceneMode.Single);
        AudioManager.instance.PlayMusic("Tune 1", 2f);
        DOVirtual.DelayedCall(0.5f, ()=>
        {
            GameController.instance.SpawnLDNFuckwit(currentFuckwit);
            Destroy(gacha.breakRoot.gameObject);
        });
    }
}
