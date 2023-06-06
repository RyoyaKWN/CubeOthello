using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class Top1_1 : MonoBehaviour
// {
//     private TileData tileData;

//     void Start()
//     {
//         tileData = GetComponent<TileData>();
//         tileData.nextTiles = new string[,] {{"Top2_1","Back4_4"}};
//     }
// }

public class Top1_1 : TileData
{
    public Top1_1(){
        // this.nextTiles = new TileData[,] {{GameObject.Find("Top2_1").GetComponent<TileData>(),
        //                                     GameObject.Find("Back4_4").GetComponent<TileData>()}};
    }
}