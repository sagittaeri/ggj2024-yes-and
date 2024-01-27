using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class GameController : MonoBehaviour
{
    [SerializeField] private Transform _startPos;
    [SerializeField] private Camera _camera;
    public Transform Arrow;
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
    public FuckwitCatalogue FuckWits = new();

    public LDNEntity placeholderRef;

    public LauncherBarUI UIRef;

    static public GameController instance;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        _camera = Camera.main;
        // LDNEntity a = Instantiate(placeholderRef, _startPos.position, _startPos.rotation);
        // a.Controller = this;
        // UIRef.Entity = a;
        
        // _camera.GetComponent<CameraFollowScript>().Player = a.RagDollTorso;
    }

    public void SpawnLDNFuckwit(FuckWits oxygenThief)
    {
        GameObject a = Instantiate(FuckWits[oxygenThief],_startPos.position, _startPos.rotation);
       
        UIRef.Entity = a.GetComponent<LDNEntity>();
        UIRef.Entity.Controller = this;
        _camera.GetComponent<CameraFollowScript>().Player = UIRef.Entity.RagDollTorso;
        UIRef.Init(UIRef.Entity);
    }
}

[System.Serializable]
public class FuckwitCatalogue : UnitySerializedDictionary<FuckWits, GameObject> { }