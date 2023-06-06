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
    public bool isSet; //石が置けるかどうかの判定
    public TileData[,] nextTiles;

    public int getTileNum(int face, int row, int col){
        int tileNum = 0;
        tileNum = face * 16 + row * 4 + col + 1;
        return tileNum;
    }

    public void drawBorder()
    {
        GameObject borderObject = Instantiate((GameObject)Resources.Load("Prefabs/Border"), gameObject.transform);
    }

    public void putStoneWhite()
    {
        Transform parentFace = gameObject.transform.parent;
        Vector3 spawnPosition = gameObject.transform.position + new Vector3(0f, 0.5f, 0f);
        GameObject newObject = Instantiate((GameObject)Resources.Load("Prefabs/StoneWhite"), spawnPosition, parentFace.rotation, gameObject.transform);
        this.state = -1;
    }

    public void putStoneBlack()
    {
        Transform parentFace = gameObject.transform.parent;
        Vector3 spawnPosition = gameObject.transform.position + new Vector3(0f, 0.5f, 0f);
        GameObject newObject = Instantiate((GameObject)Resources.Load("Prefabs/StoneBlack"), spawnPosition, parentFace.rotation, gameObject.transform);
        this.state = 1;
    }
}