using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Face
{
    Top,
    Bottom,
    Right,
    Left,
    Front,
    Back
}

public class TileData : MonoBehaviour
{
    public Face face; //面番号 0:Top, 1:Bottom, 2:Right, 3:Left, 4:Front, 5:Back
    public int row; //行番号
    public int col; //列番号
    public int state; //状態 0:空, -1:白, 1:黒
    public TileData[,] nextTiles;
}