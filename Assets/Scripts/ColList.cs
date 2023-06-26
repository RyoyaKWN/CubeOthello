using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColList : MonoBehaviour
{
    public GameObject[] tiles;
    public TileData[] tileDatas;
    public List<CircArray<TileData>> colList;

    //----- 辺が隣接 -----//
    public CircArray<TileData> x1;
    public CircArray<TileData> x2;
    public CircArray<TileData> x3;
    public CircArray<TileData> x4;
    public CircArray<TileData> y1;
    public CircArray<TileData> y2;
    public CircArray<TileData> y3;
    public CircArray<TileData> y4;
    public CircArray<TileData> z1;
    public CircArray<TileData> z2;
    public CircArray<TileData> z3;
    public CircArray<TileData> z4;

    //----- 斜め（角あり） -----//
    public CircArray<TileData> to1;
    public CircArray<TileData> to2;
    public CircArray<TileData> bo1;
    public CircArray<TileData> bo2;
    public CircArray<TileData> ri1;
    public CircArray<TileData> ri2;
    public CircArray<TileData> le1;
    public CircArray<TileData> le2;
    public CircArray<TileData> fr1;
    public CircArray<TileData> fr2;
    public CircArray<TileData> ba1;
    public CircArray<TileData> ba2;

    //----- 斜め（角なし） -----//
    public CircArray<TileData> n1;
    public CircArray<TileData> n2;
    public CircArray<TileData> n3;
    public CircArray<TileData> n4;
    public CircArray<TileData> n5;
    public CircArray<TileData> n6;
    public CircArray<TileData> r1;
    public CircArray<TileData> r2;
    public CircArray<TileData> r3;
    public CircArray<TileData> r4;
    public CircArray<TileData> r5;
    public CircArray<TileData> r6;

    
    void Start()
    {
        // tiles = GameObject.FindGameObjectsWithTag("Tile");
        tileDatas = FindObjectOfType<TileManager>().tileDatas;
        // for (int i = 0; i < tiles.Length; i++){
        //     tileDatas[i] = tiles[i].GetComponent<TileData>();
        // }

        //----- 辺が隣接 -----//
        x1 = new CircArray<TileData>(new TileData[] {
            tileDatas[3],
            tileDatas[7],
            tileDatas[11],
            tileDatas[15],
            tileDatas[67],
            tileDatas[71],
            tileDatas[75],
            tileDatas[79],
            tileDatas[16],
            tileDatas[20],
            tileDatas[24],
            tileDatas[28],
            tileDatas[80],
            tileDatas[81],
            tileDatas[82],
            tileDatas[83]
        });
        x2 = new CircArray<TileData>(new TileData[] {
            tileDatas[2],
            tileDatas[6],
            tileDatas[10],
            tileDatas[14],
            tileDatas[66],
            tileDatas[70],
            tileDatas[74],
            tileDatas[78],
            tileDatas[17],
            tileDatas[21],
            tileDatas[25],
            tileDatas[29],
            tileDatas[84],
            tileDatas[85],
            tileDatas[86],
            tileDatas[87]
        });
        x3 = new CircArray<TileData>(new TileData[] {
            tileDatas[1],
            tileDatas[5],
            tileDatas[9],
            tileDatas[13],
            tileDatas[65],
            tileDatas[69],
            tileDatas[73],
            tileDatas[77],
            tileDatas[18],
            tileDatas[22],
            tileDatas[26],
            tileDatas[30],
            tileDatas[88],
            tileDatas[89],
            tileDatas[90],
            tileDatas[91]
        });
        y1 = new CircArray<TileData>(new TileData[] {
            tileDatas[83],
            tileDatas[87],
            tileDatas[91],
            tileDatas[95],
            tileDatas[51],
            tileDatas[55],
            tileDatas[59],
            tileDatas[63],
            tileDatas[64],
            tileDatas[65],
            tileDatas[66],
            tileDatas[67],
            tileDatas[32],
            tileDatas[33],
            tileDatas[34],
            tileDatas[35]
        });
        y2 = new CircArray<TileData>(new TileData[] {
            tileDatas[82],
            tileDatas[86],
            tileDatas[90],
            tileDatas[94],
            tileDatas[50],
            tileDatas[54],
            tileDatas[58],
            tileDatas[62],
            tileDatas[68],
            tileDatas[69],
            tileDatas[70],
            tileDatas[71],
            tileDatas[36],
            tileDatas[37],
            tileDatas[38],
            tileDatas[39]
        });
        y3 = new CircArray<TileData>(new TileData[] {
            tileDatas[81],
            tileDatas[85],
            tileDatas[89],
            tileDatas[93],
            tileDatas[49],
            tileDatas[53],
            tileDatas[57],
            tileDatas[61],
            tileDatas[72],
            tileDatas[73],
            tileDatas[74],
            tileDatas[75],
            tileDatas[40],
            tileDatas[41],
            tileDatas[42],
            tileDatas[43]
        });
        y4 = new CircArray<TileData>(new TileData[] {
            tileDatas[80],
            tileDatas[84],
            tileDatas[88],
            tileDatas[92],
            tileDatas[48],
            tileDatas[52],
            tileDatas[56],
            tileDatas[60],
            tileDatas[76],
            tileDatas[77],
            tileDatas[78],
            tileDatas[79],
            tileDatas[44],
            tileDatas[45],
            tileDatas[46],
            tileDatas[47]
        });
        x4 = new CircArray<TileData>(new TileData[] {
            tileDatas[0],
            tileDatas[4],
            tileDatas[8],
            tileDatas[12],
            tileDatas[64],
            tileDatas[68],
            tileDatas[72],
            tileDatas[76],
            tileDatas[19],
            tileDatas[23],
            tileDatas[27],
            tileDatas[31],
            tileDatas[92],
            tileDatas[93],
            tileDatas[94],
            tileDatas[95]
        });
        z1 = new CircArray<TileData>(new TileData[] {
            tileDatas[0],
            tileDatas[1],
            tileDatas[2],
            tileDatas[3],
            tileDatas[35],
            tileDatas[39],
            tileDatas[43],
            tileDatas[47],
            tileDatas[28],
            tileDatas[29],
            tileDatas[30],
            tileDatas[31],
            tileDatas[48],
            tileDatas[49],
            tileDatas[50],
            tileDatas[51]
        });
        z2 = new CircArray<TileData>(new TileData[] {
            tileDatas[4],
            tileDatas[5],
            tileDatas[6],
            tileDatas[7],
            tileDatas[34],
            tileDatas[38],
            tileDatas[42],
            tileDatas[46],
            tileDatas[24],
            tileDatas[25],
            tileDatas[26],
            tileDatas[27],
            tileDatas[52],
            tileDatas[53],
            tileDatas[54],
            tileDatas[55]
        });
        z3 = new CircArray<TileData>(new TileData[] {
            tileDatas[8],
            tileDatas[9],
            tileDatas[10],
            tileDatas[11],
            tileDatas[33],
            tileDatas[37],
            tileDatas[41],
            tileDatas[45],
            tileDatas[20],
            tileDatas[21],
            tileDatas[22],
            tileDatas[23],
            tileDatas[56],
            tileDatas[57],
            tileDatas[58],
            tileDatas[59]
        });
        z4 = new CircArray<TileData>(new TileData[] {
            tileDatas[12],
            tileDatas[13],
            tileDatas[14],
            tileDatas[15],
            tileDatas[32],
            tileDatas[36],
            tileDatas[40],
            tileDatas[44],
            tileDatas[16],
            tileDatas[17],
            tileDatas[18],
            tileDatas[19],
            tileDatas[60],
            tileDatas[61],
            tileDatas[62],
            tileDatas[63]
        });

        //----- 斜め（角あり） -----//
        to1 = new CircArray<TileData>(new TileData[] {
            tileDatas[0],
            tileDatas[5],
            tileDatas[10],
            tileDatas[15],
            tileDatas[96]
        });

        to2 = new CircArray<TileData>(new TileData[] {
            tileDatas[3],
            tileDatas[6],
            tileDatas[9],
            tileDatas[12],
            tileDatas[96]
        });

        bo1 = new CircArray<TileData>(new TileData[] {
            tileDatas[16],
            tileDatas[21],
            tileDatas[26],
            tileDatas[31],
            tileDatas[96]
        });

        bo2 = new CircArray<TileData>(new TileData[] {
            tileDatas[19],
            tileDatas[22],
            tileDatas[25],
            tileDatas[28],
            tileDatas[96]
        });

        ri1 = new CircArray<TileData>(new TileData[] {
            tileDatas[32],
            tileDatas[37],
            tileDatas[42],
            tileDatas[47],
            tileDatas[96]
        });

        ri2 = new CircArray<TileData>(new TileData[] {
            tileDatas[35],
            tileDatas[38],
            tileDatas[41],
            tileDatas[44],
            tileDatas[96]
        });

        le1 = new CircArray<TileData>(new TileData[] {
            tileDatas[48],
            tileDatas[53],
            tileDatas[58],
            tileDatas[63],
            tileDatas[96]
        });

        le2 = new CircArray<TileData>(new TileData[] {
            tileDatas[60],
            tileDatas[57],
            tileDatas[54],
            tileDatas[51],
            tileDatas[96]
        });

        fr1 = new CircArray<TileData>(new TileData[] {
            tileDatas[64],
            tileDatas[69],
            tileDatas[74],
            tileDatas[79],
            tileDatas[96]
        });

        fr2 = new CircArray<TileData>(new TileData[] {
            tileDatas[67],
            tileDatas[70],
            tileDatas[73],
            tileDatas[76],
            tileDatas[96]
        }); 
        
        ba1 = new CircArray<TileData>(new TileData[] {
            tileDatas[80],
            tileDatas[85],
            tileDatas[90],
            tileDatas[95],
            tileDatas[96]
        });
        
        ba2 = new CircArray<TileData>(new TileData[] {
            tileDatas[92],
            tileDatas[89],
            tileDatas[86],
            tileDatas[83],
            tileDatas[96]
        }); 

        //----- 斜め（角なし） -----//
        n1 = new CircArray<TileData>(new TileData[] {
            tileDatas[0],
            tileDatas[91],
            tileDatas[86],
            tileDatas[81],
            tileDatas[47],
            tileDatas[24],
            tileDatas[21],
            tileDatas[18],
            tileDatas[76],
            tileDatas[61],
            tileDatas[58],
            tileDatas[55]
        });
        n2 = new CircArray<TileData>(new TileData[] {
            tileDatas[1],
            tileDatas[87],
            tileDatas[82],
            tileDatas[43],
            tileDatas[46],
            tileDatas[20],
            tileDatas[17],
            tileDatas[77],
            tileDatas[72],
            tileDatas[62],
            tileDatas[59],
            tileDatas[4],
        });
        n3 = new CircArray<TileData>(new TileData[] {
            tileDatas[2],
            tileDatas[83],
            tileDatas[39],
            tileDatas[42],
            tileDatas[45],
            tileDatas[16],
            tileDatas[78],
            tileDatas[73],
            tileDatas[68],
            tileDatas[63],
            tileDatas[8],
            tileDatas[5]
        });
        n4 = new CircArray<TileData>(new TileData[] {
            tileDatas[15],
            tileDatas[66],
            tileDatas[69],
            tileDatas[72],
            tileDatas[60],
            tileDatas[23],
            tileDatas[26],
            tileDatas[29],
            tileDatas[80],
            tileDatas[43],
            tileDatas[38],
            tileDatas[33]
        });
        n5 = new CircArray<TileData>(new TileData[] {
            tileDatas[14],
            tileDatas[65],
            tileDatas[68],
            tileDatas[61],
            tileDatas[56],
            tileDatas[27],
            tileDatas[30],
            tileDatas[84],
            tileDatas[81],
            tileDatas[39],
            tileDatas[34],
            tileDatas[11]
        });
        n6 = new CircArray<TileData>(new TileData[] {
            tileDatas[13],
            tileDatas[64],
            tileDatas[62],
            tileDatas[57],
            tileDatas[52],
            tileDatas[31],
            tileDatas[88],
            tileDatas[85],
            tileDatas[82],
            tileDatas[35],
            tileDatas[7],
            tileDatas[10]
        });
        r1 = new CircArray<TileData>(new TileData[] {
            tileDatas[3],
            tileDatas[34],
            tileDatas[37],
            tileDatas[40],
            tileDatas[79],
            tileDatas[17],
            tileDatas[22],
            tileDatas[27],
            tileDatas[48],
            tileDatas[93],
            tileDatas[90],
            tileDatas[87]
        });
        r2 = new CircArray<TileData>(new TileData[] {
            tileDatas[7],
            tileDatas[33],
            tileDatas[36],
            tileDatas[75],
            tileDatas[78],
            tileDatas[18],
            tileDatas[23],
            tileDatas[52],
            tileDatas[49],
            tileDatas[94],
            tileDatas[91],
            tileDatas[2]
        });
        r3 = new CircArray<TileData>(new TileData[] {
            tileDatas[11],
            tileDatas[32],
            tileDatas[71],
            tileDatas[74],
            tileDatas[77],
            tileDatas[19],
            tileDatas[56],
            tileDatas[53],
            tileDatas[50],
            tileDatas[95],
            tileDatas[1],
            tileDatas[6]
        });
        r4 = new CircArray<TileData>(new TileData[] {
            tileDatas[12],
            tileDatas[59],
            tileDatas[54],
            tileDatas[49],
            tileDatas[92],
            tileDatas[30],
            tileDatas[25],
            tileDatas[20],
            tileDatas[44],
            tileDatas[75],
            tileDatas[70],
            tileDatas[65]
        });
        r5 = new CircArray<TileData>(new TileData[] {
            tileDatas[8],
            tileDatas[55],
            tileDatas[50],
            tileDatas[93],
            tileDatas[88],
            tileDatas[29],
            tileDatas[24],
            tileDatas[45],
            tileDatas[40],
            tileDatas[71],
            tileDatas[66],
            tileDatas[13]
        });
        r6 = new CircArray<TileData>(new TileData[] {
            tileDatas[4],
            tileDatas[51],
            tileDatas[94],
            tileDatas[89],
            tileDatas[84],
            tileDatas[28],
            tileDatas[46],
            tileDatas[41],
            tileDatas[36],
            tileDatas[67],
            tileDatas[14],
            tileDatas[9]
        });

        //----- 列情報をまとめたリスト -----//
        colList = new List<CircArray<TileData>>() {x1, x2, x3, x4, y1, y2, y3, y4, z1, z2, z3, z4, to1, to2, bo1, bo2, ri1, ri2, le1, le2, fr1, fr2, ba1, ba2, n1, n2, n3, n4, n5, n6, r1, r2, r3, r4, r5, r6};
    }
}
