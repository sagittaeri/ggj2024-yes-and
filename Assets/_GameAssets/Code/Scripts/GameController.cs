using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;


public class GameController : MonoBehaviour
{
    [SerializeField] private Transform _startPos;
    [SerializeField] private Camera _camera;
    public Transform Arrow;
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
    public FuckwitCatalogue FuckWits = new();

    public LDNEntity placeholderRef;

    public LauncherBarUI UIRef;
    public Transform golf;
    static public GameController instance;
    [SerializeField] private GridTerrain terrainGenerator;
    [NonSerialized] public Vector3 golfStartAngle;

    public CameraFollowScript cameraFollow;

    float startZ;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        _camera = Camera.main;
        // LDNEntity a = Instantiate(placeholderRef, _startPos.position, _startPos.rotation);
        // a.Controller = this;
        // UIRef.Entity = a;
        
        // _camera.GetComponent<CameraFollowScript>().Player = a.RagDollTorso;
        golfStartAngle = golf.transform.eulerAngles;
    }

    public void SpawnLDNFuckwit(FuckWits oxygenThief)
    {
        GameObject a = Instantiate(FuckWits[oxygenThief],_startPos.position, _startPos.rotation);
       
        
        UIRef.Entity = a.GetComponent<LDNEntity>();
        UIRef.Entity.Controller = this;
        cameraFollow = _camera.GetComponent<CameraFollowScript>();
        cameraFollow.Player = UIRef.Entity.RagDollTorso;
        UIRef.Init(UIRef.Entity);
        terrainGenerator.player = UIRef.Entity.RagDollTorso.gameObject;
        startZ = UIRef.Entity.RagDollTorso.transform.position.z;
        UIRef.Entity.RagDollTorso.AddComponent<ColliderHandler>();
        UIRef.Entity.golf = golf;
        UIRef.Entity.NewRotGolf = golf.eulerAngles;
        golf.transform.DOKill();
        golf.transform.eulerAngles = UIRef.Entity.NewRotGolf;
    }

    public void UpdateDistance()
    {
        UIRef.SetDistanceText(UIRef.Entity.RagDollTorso.transform.position.z - startZ);
    }
}

[System.Serializable]
public class FuckwitCatalogue : UnitySerializedDictionary<FuckWits, GameObject> { }