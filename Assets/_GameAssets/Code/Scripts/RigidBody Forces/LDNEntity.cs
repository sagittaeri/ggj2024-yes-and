using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LDNEntity : MonoBehaviour
{
    [SerializeField] private bool _ragDollActive = false;
    public GameController Controller;
    private Rigidbody _rb => GetComponent<Rigidbody>();
    [SerializeField] private float _launchStrength = 100;
    [SerializeField] private Rigidbody _ragdollTorso;
    [SerializeField] private ForceMode _forceType;
    [SerializeField] private float _nudgeAmount = 0.5f;

    //Launcher Multiplier and direction
    [SerializeField] private float _launchMult = 1;
    [SerializeField] private Vector3 _launchDir;
    [SerializeField] private float _maxLauncherSpeed = 8;

    private int _launchStage = 0;
    private float pingpongMultTemp = 0;
    [SerializeField] private float pingpongMult;
    [SerializeField] private float pingpongDir;

    public Transform RagDollTorso => _ragdollTorso.transform;
    public System.Action<float> onPowerSet;
    public System.Action<float> onDirSet;

    public void OnPowerSet(float a)
    {
        onPowerSet?.Invoke(a);
    }

    public void OnDirSet(float a)
    {
        onDirSet?.Invoke(a);
    }
    private void Awake()
    {
        if (!_ragdollTorso)
            Debug.LogError("Torso Ragdoll Unassigned.");
        
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            _ragDollActive = false;
            _rb.isKinematic = false;
            _rb.useGravity = true;
            
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    void Update()
    {
        Launcher();
        GameController.instance.UpdateDistance();

        
        if (_ragdollTorso.velocity.magnitude < 0.1f)
            Debug.Log("Stopped");
        //Steering.
        _ragdollTorso.AddForce(transform.right * _nudgeAmount * Input.GetAxis("Horizontal"),_forceType);

    }


    private void OnCollisionEnter(Collision other)
    {
        //throw new NotImplementedException();
    }

    void Launcher()
    {
        if (_launchStage >= 2)
            return;
        _launchDir.y = Mathf.Clamp(_launchDir.y +Input.GetAxis("Horizontal")/2, -45, 45);
        OnDirSet((_launchDir.y/90)+0.5f);
        
        if (Controller)
            Controller.Arrow.localEulerAngles = new Vector3(90,0,-_launchDir.y);
        
        if (Input.GetButtonDown("Horizontal"))
        {
            AudioManager.instance.PlaySFX("aim left");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            AudioManager.instance.PlaySFX("aim club");
            AudioManager.instance.PlayMusic("Club Loop");
        }

        if (Input.GetButton("Fire1"))
        {
            pingpongMult = Mathf.Clamp(pingpongMult + Time.deltaTime * 2, 1, _maxLauncherSpeed);
            OnPowerSet((pingpongMult/_maxLauncherSpeed));
            if (pingpongMult >= _maxLauncherSpeed)
            {
                pingpongMult = _maxLauncherSpeed;
                if (Controller)
                    Controller.Arrow.gameObject.SetActive(false);
                _ragDollActive = true;
                _launchStage = 2;
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            _ragDollActive = true;
            _launchStage = 2;
            if (Controller)
                Controller.Arrow.gameObject.SetActive(false);
        }
        
        if (_ragDollActive && _ragdollTorso)
        {
            foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            {
                _ragDollActive = false;
                _rb.isKinematic = true;
                _rb.useGravity = false;
                transform.localEulerAngles = _launchDir;
                _ragdollTorso.AddForce((transform.forward + transform.up) * (_launchStrength*(1+pingpongMult)),ForceMode.VelocityChange);
                
                rb.isKinematic = false;
                rb.useGravity = true;

                TextEffectManager.instance.CreateEffect(TextEffectManager.AnimStyle.Bang);
                AudioManager.instance.PlayMusic("Tune 2", 0.5f);
            }
        }
    }
}
