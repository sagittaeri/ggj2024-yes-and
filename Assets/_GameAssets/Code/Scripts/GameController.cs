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
    
    // Start is called before the first frame update
    void Awake()
    {
        _camera = Camera.main;
        LDNEntity a = Instantiate(placeholderRef, _startPos.position, _startPos.rotation);
        UIRef.Entity = a;
        a.Controller = this;
        _camera.GetComponent<CameraFollowScript>().Player = a.RagDollTorso;
        
    }

    public void SpawnLDNFuckwit(FuckWits oxygenThief)
    {
        Instantiate(FuckWits[oxygenThief],_startPos.position, _startPos.rotation);
    }
}

[System.Serializable]
public class FuckwitCatalogue : UnitySerializedDictionary<FuckWits, GameObject> { }

