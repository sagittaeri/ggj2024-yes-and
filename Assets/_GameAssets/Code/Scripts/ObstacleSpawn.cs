using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spawnablePrefabs = new();

    [SerializeField] private List<GameObject> _chunk = new();
    
    
    private void Start()
    {
        
    }
}
