using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LauncherBarUI : MonoBehaviour
{
    public TextMeshProUGUI distanceText;

    [SerializeField] private Scrollbar _dirLaunch;
    [SerializeField] private Image _powerBar;
    [SerializeField] private LDNEntity entity;
    public LDNEntity Entity
    {
        get => entity;
        set => entity = value;
    }

    void Awake()
    {
        gameObject.SetActive(false);
        SetDistanceText(0f);
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
    }
}
