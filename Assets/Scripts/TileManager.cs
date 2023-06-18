using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject[] tiles;
    public TileData[] tileDatas;
    void Start()
    {
        int i = 0;
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        tileDatas = new TileData[tiles.Length];
        
        for (int face = 0; face < 6; face++)
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    // tiles[i].AddComponent<DrawBorder>();
                    TileData tileData = tiles[i].AddComponent<TileData>();
                    tileData.face = (Face)Enum.ToObject(typeof(Face), face);
                    tileData.row = row;
                    tileData.col = col;
                    tileData.state = 0;
                    tileData.isSet = false;
                    tileDatas[i] = tileData;
                    i++;
                }
            }
        }
    }
}