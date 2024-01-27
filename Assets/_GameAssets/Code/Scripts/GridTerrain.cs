using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CodingWithRus / goldennoodles
public class GridTerrain : MonoBehaviour
{
    private int terrainState = 0;
    public List<GameObject> planeList;
    public GameObject player;

    private int radius = 10;
    private int planeOffset = 40;

    private Vector3 startPos = Vector3.zero;

    private int XPlayerMove => (int)(player.transform.position.x - startPos.x);
    private int ZPlayerMove => (int)(player.transform.position.z - startPos.z);

    private int XPlayerLocation;
    private int ZPlayerLocation, prevZPlayerLocation;

    Hashtable tilePlane = new Hashtable();

    public void jankyfix()
    {
        XPlayerLocation= (int)Mathf.Floor(player.transform.position.x / planeOffset) * planeOffset;
        ZPlayerLocation= (int)Mathf.Floor(player.transform.position.z / planeOffset) * planeOffset;
    }
    
    void Update()
    {
        generateWorld();
    }

    private void LateUpdate()
    {
        if (prevZPlayerLocation != ZPlayerLocation)
        {
            prevZPlayerLocation = ZPlayerLocation;
            if (ZPlayerLocation % 32 == 0)
            {
                if (terrainState > 5)
                    terrainState = 0;
                else
                    terrainState++;
            }

            

        }
        
    }

    private void generateWorld ()
    {

        if (player != null)
            jankyfix();
        if(startPos == Vector3.zero||hasPlayerMoved(XPlayerMove, ZPlayerMove))
        {

            CreateTile();
        }
    }

    private void CreateTile()
    {
        for (int x = -radius; x < radius; x++)
        {
            

            for (int z = -radius; z < radius; z++)
            {
                
                Vector3 pos = new Vector3((x * planeOffset + XPlayerLocation), 0, (z * planeOffset + ZPlayerLocation));
                
                if (!tilePlane.Contains(pos))
                {


                    GameObject tile = Instantiate(planeList[terrainState], pos, Quaternion.identity);
                    tilePlane.Add(pos, tile);
                }
            }
        }
    }

    private bool hasPlayerMoved(int playerX, int playerZ)
    {
       
        return (Mathf.Abs(XPlayerMove) >= planeOffset || Mathf.Abs(ZPlayerMove) >= planeOffset);
    }
}