using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
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
    }
}