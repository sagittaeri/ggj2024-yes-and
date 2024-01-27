using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CodingWithRus / goldennoodles
public class GridTerrain : MonoBehaviour
{
    public GameObject planeList;
    public GameObject player;

    private int radius = 10;
    private int planeOffset = 1080;

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
            for (int y = -radius; y < radius; x++)
            {
                Vector3 pos = new Vector3((XPlayerLocation), 0, (x * planeOffset + ZPlayerLocation));

                if (!tilePlane.Contains(pos))
                {
                    GameObject tile = Instantiate(planeList, pos, Quaternion.identity);
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