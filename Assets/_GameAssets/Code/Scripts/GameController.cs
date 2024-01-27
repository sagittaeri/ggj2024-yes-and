using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class GameController : MonoBehaviour
{
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
    public FuckwitCatalogue FuckWits = new(); 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}

[System.Serializable]
public class FuckwitCatalogue : UnitySerializedDictionary<FuckWits, GameObject> { }

