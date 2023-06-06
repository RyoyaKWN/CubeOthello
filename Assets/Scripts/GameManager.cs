using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentTurn = 1;
    public int countBlack;
    public int countWhite;
    public List<TileData> tileDatas;

    void Start()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        tileDatas = new List<TileData>();
        foreach(GameObject tile in tiles)
        {
            tileDatas.Add(tile.GetComponent<TileData>());
        }
    }

    void Update()
    {
        
    }

    public void changeTurn()
    {
        currentTurn *= -1;
    }

    public List<CircArray<TileData>> colListOfTile(TileData targetTile)
    {
        List<CircArray<TileData>> colList = new List<CircArray<TileData>>();
        foreach(CircArray<TileData> tiles in FindObjectOfType<ColList>().colList)
        {
            foreach(TileData tile in tiles)
            if(tile == targetTile)
            {
                colList.Add(tiles);
            }
        }

        return colList;
    }

    public bool isValidMoves(TileData targetTile, int turn)
    {
        if(targetTile.state != 0){
            return false;
        }
        List<CircArray<TileData>> colList = colListOfTile(targetTile);
        foreach(CircArray<TileData> tiles in colList)
        {
            int i = 0;
            while(tiles[i] != targetTile){
                i++;
            }
            int di = 1;
            while(tiles[i+di].state == -turn)
            {
                di++;
            }
            if(di > 1 && tiles[i+di].state == turn)
            {
                return true;
            }
            di = 1;
            while( tiles[i-di].state == -turn)
            {
                di++;
            }
            if(di > 1 && tiles[i-di].state == turn)
            {
                return true;
            }
        }

        return false;
    }

    public List<TileData> getValidMoves(int turn)
    {
        List<TileData> validMoves = new List<TileData>();
        foreach(TileData tile in tileDatas)
        {
            if(isValidMoves(tile, turn))
            {
                validMoves.Add(tile);
            }
        }

        return validMoves;
    }

    private void flip(TileData targetTile, int turn)
    {
        List<TileData> flipTiles = new List<TileData>();
        List<CircArray<TileData>> colList = colListOfTile(targetTile);
        foreach(CircArray<TileData> tiles in colList)
        {
            int i = 0;
            while(tiles[i] != targetTile){
                i++;
            }
            int di = 1;
            while(tiles[i+di].state == -turn)
            {
                di++;
            }
            if(di > 1 && tiles[i+di].state == turn)
            {
                while(di > 1)
                {
                    flipTiles.Add(tiles[i+(di-1)]);
                    di--;
                }
            }
            di = 1;
            while(tiles[i-di].state == -turn)
            {
                di++;
            }
            if(di > 1 && tiles[i-di].state == turn)
            {
                while(di > 1)
                {
                    flipTiles.Add(tiles[i-(di-1)]);
                    di--;
                }
            }
        }

        foreach(TileData flipTile in flipTiles)
        {
            changeStone(flipTile);
        }

    }
    public void putStone(TileData targetTile, int turn){
        if(turn == 1)
        {
            targetTile.state = -1;
        }else if(turn == -1)
        {
            targetTile.state = 1;
        }
    }

    public void changeStone(TileData targetTile)
    {
        putStone(targetTile, -targetTile.state);
        targetTile.state *= -1;
    }
}