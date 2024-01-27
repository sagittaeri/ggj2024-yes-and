using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;


public class TileSpawnablePrefabs : MonoBehaviour
{
    [SerializeField] private List<SpawnObjectsTerrain> TerrainPrefabs = new();

    private void Awake()
    {
        foreach (var a in TerrainPrefabs)
        {
            for (int i = 0; i < a.frequnecy; i++)
            {
                Vector3 random = new Vector3(Random.Range(0,-1080), 0, Random.Range(0, -100));
                Instantiate(a.SpawnableObjects, transform.TransformPoint(random), a.SpawnableObjects.transform.rotation);
            }
        }
    }
}
[Serializable]
public class SpawnObjectsTerrain
{
    public GameObject SpawnableObjects;
    public float frequnecy;
}