using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnableRagdoll : MonoBehaviour
{
    [SerializeField] private bool _ragDollActive = false;

    private Rigidbody _rb => GetComponent<Rigidbody>();
    [SerializeField] private float _launchStrength = 2;
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


        //Steering.
        /*_ragdollTorso.AddForce(transform.right * _nudgeAmount * Input.GetAxis("Horizontal"),_forceType);*/

    }

    void Launcher()
    {
        _launchDir.y = Mathf.Clamp(_launchDir.y +Input.GetAxis("Horizontal"), -45, 45);
        if (Input.GetButton("Fire1"))
        {
            pingpongMult = Mathf.Clamp(pingpongMult + Time.deltaTime * 2, 0, _maxLauncherSpeed);
            if (pingpongMult >= _maxLauncherSpeed)
            {
                pingpongMult = _maxLauncherSpeed;
                
                _ragDollActive = true;
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            _ragDollActive = true;
        }

        _launchStage = 2;
        
        if (_ragDollActive && _ragdollTorso)
        {
            foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            {
                _ragDollActive = false;
                _rb.isKinematic = true;
                _rb.useGravity = false;
                transform.localEulerAngles = _launchDir;
                _ragdollTorso.AddForce((transform.forward + transform.up) * (_launchStrength*pingpongMult),ForceMode.VelocityChange);
                
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        }
    }
    
    void OriginalLauncherCode()
    {
        if (_launchStage == 0)
            pingpongMult = Mathf.PingPong(Time.time * 100, 50);
        if (_launchStage == 1)
            pingpongDir = Mathf.Lerp (-45, 45, Mathf.PingPong(Time.time, 1));

        if (Input.GetButtonDown("Jump"))
        {
            switch (_launchStage)
            {
                case 0: _launchMult = pingpongMult;
                    _launchStage++;
                    break;
                case 1:
                    _launchDir.y = pingpongDir;
                    _launchStage++;
                    break;
                
                case 2: _ragDollActive = true;
                    _launchStage++;
                    break;
                default:
                    _launchStage = 4;
                    break;
            }
            
        }
    }
}
