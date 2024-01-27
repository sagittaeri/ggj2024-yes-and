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
    private int radiusY = 2;
    private int planeOffsetY = 1080;
    private int planeOffsetX = 800;

    private Vector3 startPos = Vector3.zero;

    private int XPlayerMove => (int)(player.transform.position.x - startPos.x);
    private int ZPlayerMove => (int)(player.transform.position.z - startPos.z);

    private int XPlayerLocation;
    private int ZPlayerLocation, prevZPlayerLocation;

    Hashtable tilePlane = new();

    public void jankyfix()
    {
        XPlayerLocation= (int)Mathf.Floor(player.transform.position.x / planeOffsetY) * planeOffsetY;
        ZPlayerLocation= (int)Mathf.Floor(player.transform.position.z / planeOffsetY) * planeOffsetY;
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
        for (int x = -radiusY; x < radiusY; x++)
        {
            for (int y = -radiusY; y < radiusY; y++)
            {
                Vector3 pos = new Vector3(x * planeOffsetX + XPlayerLocation, 0, (y * planeOffsetY)+ ZPlayerLocation);

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
        return (Mathf.Abs(playerX) >= planeOffsetX || Mathf.Abs(playerZ) >= planeOffsetY);
    }
}