using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LauncherBarUI : MonoBehaviour
{
    [SerializeField] private Scrollbar _dirLaunch;
    [SerializeField] private Image _powerBar;
    [SerializeField] private LDNEntity entity;
    public LDNEntity Entity
    {
        get => entity;
        set => entity = value;
    }

    private void OnEnable()
    {
        entity.onPowerSet += LaunchPowerUI;
        entity.onDirSet += LaunchDirUI;
    }

    private void OnDisable()
    {
        entity.onPowerSet -= LaunchPowerUI;
        entity.onDirSet -= LaunchDirUI;
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
}
