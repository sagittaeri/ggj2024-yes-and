using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LauncherBarUI : MonoBehaviour
{
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI distanceText2;
    public GameObject victoryScreen;
    public Transform starTransform;

    [SerializeField] private Scrollbar _dirLaunch;
    [SerializeField] private Image _powerBar;
    [SerializeField] private LDNEntity entity;
    public LDNEntity Entity
    {
        get => entity;
        set => entity = value;
    }

    private float timeRotate = -1f;
    public float breakRotateInterval = 0.5f;

    void Awake()
    {
        gameObject.SetActive(false);
        SetDistanceText(0f);
        victoryScreen.SetActive(false);
    }

    void Update()
    {
        if (starTransform.gameObject.activeInHierarchy)
        {
            if (timeRotate < Time.timeSinceLevelLoad)
            {
                timeRotate = Time.timeSinceLevelLoad + breakRotateInterval;
                starTransform.localEulerAngles = new Vector3(0f, 0f, UnityEngine.Random.Range(0f, 360f));
            }
        }
    }

    private void OnDisable()
    {
        if (entity != null)
        {
            entity.onPowerSet -= LaunchPowerUI;
            entity.onDirSet -= LaunchDirUI;
        }
    }


    public void Init(LDNEntity _entity)
    {
        entity = _entity;
        entity.onPowerSet += LaunchPowerUI;
        entity.onDirSet += LaunchDirUI;
        gameObject.SetActive(true);
    }
    
    // Update is called once per frame
    public void LaunchPowerUI(float amt)
    {
        _powerBar.fillAmount = amt;
    }
    public void LaunchDirUI(float amt)
    {
        _dirLaunch.value = amt;
    }

    public void SetDistanceText(float dist)
    {
        distanceText.text = $"{dist:n0}m";
        distanceText2.text = $"{dist:n0}m";
    }

    public void ShowVictory()
    {
        victoryScreen.SetActive(true);
    }
}
