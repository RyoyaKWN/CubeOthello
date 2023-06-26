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
        Transform parent = GameObject.Find("Cube").transform;
        tileDatas = new TileData[parent.childCount];
        int index = 0;
        foreach(Transform child in parent)
        {
            child.gameObject.AddComponent<TileData>();
            TileData tileData = child.gameObject.GetComponent<TileData>();
            tileData.state = 0;
            tileDatas[index] = tileData;
            index++;
        }



        // int i = 0;
        // tiles = GameObject.Find("Cube").transform.GetChildren();
        // tileDatas = new TileData[tiles.Length];
        
        // for (int face = 0; face < 6; face++)
        // {
        //     for (int row = 0; row < 4; row++)
        //     {
        //         for (int col = 0; col < 4; col++)
        //         {
        //             TileData tileData = tiles[i].AddComponent<TileData>();
        //             tileData.face = (Face)Enum.ToObject(typeof(Face), face);
        //             tileData.row = row;
        //             tileData.col = col;
        //             tileData.state = 0;
        //             tileDatas[i] = tileData;
        //             i++;
        //         }
        //     }
        // }

        // NULL_Tileの設定
        // TileData nullTileData = tileDatas[tileDatas.Length - 1];
        // nullTileData.state = 0;
        // tileDatas[tileDatas.Length - 1] = nullTileData;
    }
}