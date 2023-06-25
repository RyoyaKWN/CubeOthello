using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Camera subCamera;
    public float subScreenSize = 0.2f;
    public GameObject targetObject;
    public GameObject clickedTile;
    public GameObject stoneWhite;
    public GameObject stoneBlack;
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI countBlackText;
    public TextMeshProUGUI countWhiteText;
    public TextMeshProUGUI gameResult;
    private TileManager tileManager;

    public float rotationSpeed = 100.0f;
    private Vector3 lastMousePosition;

    private int turn = 1;
    private int countBlack;
    private int countWhite;
    private List<TileData> tileDatas;

    private Camera mainCamera;

    public ModeData modeData;
    public int CPUTurn;
    private Coroutine CPUMoveCoroutine;

    void Start()
    {
        mainCamera = Camera.main;

        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        tileDatas = new List<TileData>();
        foreach (GameObject tile in tiles)
        {
            tileDatas.Add(tile.GetComponent<TileData>());
        }
        
        putStone(GameObject.Find("Top2_2"), 1);
        putStone(GameObject.Find("Top3_3"), 1);
        putStone(GameObject.Find("Top2_3"), -1);
        putStone(GameObject.Find("Top3_2"), -1);
        foreach(TileData tile in getValidMoves(turn))
        {
            GameObject parentObject = tile.gameObject;
            drawBorder(parentObject);
        }

        // CPUTurnの取得
        CPUTurn = modeData.selectedMode;

        if(CPUTurn == 1){
            CPUMoveCoroutine = StartCoroutine(PerformCPUMove());
        }

        //---------- 列の確認 ----------//
        // foreach (TileData tile in FindObjectOfType<ColList>().r6)
        // {
        //     drawBorder(tile.gameObject);
        // }

        // List<CircArray<TileData>> targetList = new List<CircArray<TileData>>();
        // foreach (CircArray<TileData> tiles in FindObjectOfType<ColList>().colList)
        // {
        //     foreach (TileData tile in tiles)
        //     {
        //         if(tile == GameObject.Find("Back4_2").GetComponent<TileData>())
        //         {
        //             targetList.Add(tiles);
        //         }
        //     }
        // }

        // foreach (CircArray<TileData> tiles in targetList)
        // {
        //     foreach (TileData tile in tiles)
        //     {
        //         drawBorder(tile.gameObject);
        //     }
        // }
    }

    void Update()
    {
        // if(turn == CPUTurn && getValidMoves(CPUTurn).Count > 0){
        //     Debug.Log("hello");
        //     // CPUMoveCoroutine = StartCoroutine(PerformCPUMove());
        // }

        if (Input.GetMouseButtonDown(0)) {
            clickedTile = null;
            Vector3 clickPosition;
 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
 
            if (Physics.Raycast(ray, out hit)) {
                clickedTile = hit.collider.gameObject;
                clickPosition = hit.point;
                if (clickedTile.CompareTag("Tile")){
                    //プレイヤーが手番を終えた後にCPUの手番を実行する
                    if(turn == CPUTurn){
                        Debug.Log("Not Your Turn");
                        return;
                    }
                    if(isValidMoves(clickedTile.GetComponent<TileData>(), turn)){
                        putStone(clickedTile, turn);
                        flip(clickedTile.GetComponent<TileData>(), turn);


                        countStone();
                        GameObject[] borders = GameObject.FindGameObjectsWithTag("Border");
                        foreach(GameObject border in borders){
                            Destroy(border);
                        }

                        if(isGameOver()){ //勝敗判定
                            result();
                        }
                        else if(getValidMoves(-turn).Count > 0 && -turn == CPUTurn){
                            changeTurn();
                            CPUMoveCoroutine = StartCoroutine(PerformCPUMove());
                        }else{
                            changeTurn();
                        }
                        
                        //----- 石を置けるマスを再描画 -----//
                        // GameObject[] borders = GameObject.FindGameObjectsWithTag("Border");
                        // foreach(GameObject border in borders){
                        //     Destroy(border);
                        // }
                        // foreach(TileData tile in getValidMoves(turn))
                        // {
                        //     GameObject parentObject = tile.gameObject;
                        //     drawBorder(parentObject);
                        // }
                    }
                }
            }else{
                clickedTile = null;
            }

 
        }

        else if (Input.GetMouseButton(0))
        {
            if (clickedTile == null)
            {
                float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad * 5;
                float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad * 5;

                targetObject.transform.Rotate(mainCamera.transform.up, -rotX, Space.World);
                targetObject.transform.Rotate(mainCamera.transform.right, rotY, Space.World);
            }
        }

        if(turn == 1){
            turnText.faceColor = new Color(0, 0, 0, 1);
            turnText.text = "BLACK TURN";
        }else{
            turnText.faceColor = new Color(255, 255, 255, 1);
            turnText.text = "WHITE TURN";
        }
    }

    private void putStone(GameObject targetTile, int currentTurn)
    {
        TileData tileData = targetTile.GetComponent<TileData>();
        Transform parentFace = targetTile.transform.parent;

        if (currentTurn == 1){
            Vector3 spawnPosition = targetTile.transform.position + new Vector3(0f, 0.5f, 0f);
            GameObject newObject = Instantiate(stoneBlack, spawnPosition, parentFace.rotation, targetTile.transform);
            tileData.state = 1;
        }else{
            Vector3 spawnPosition = targetTile.transform.position + new Vector3(0f, 0.5f, 0f);
            GameObject newObject = Instantiate(stoneWhite, spawnPosition, parentFace.rotation, targetTile.transform);
            tileData.state = -1;
        }
    }

    public void putStoneBlack(GameObject targetTile){
        Transform face = targetTile.transform.parent;
        Vector3 spawnPosition = targetTile.transform.position + new Vector3(0f, 0.5f, 0f);
        GameObject newObject = Instantiate(stoneBlack, spawnPosition, face.rotation, targetTile.transform);    
    }

    public void putStoneWhite(GameObject targetTile){
        Transform face = targetTile.transform.parent;
        Vector3 spawnPosition = targetTile.transform.position + new Vector3(0f, 0.5f, 0f);
        GameObject newObject = Instantiate(stoneWhite, spawnPosition, face.rotation, targetTile.transform);    
    }

    private void changeStone(GameObject targetTile)
    {
        int stateOfTile = targetTile.GetComponent<TileData>().state;
        GameObject child = targetTile.transform.GetChild(0).gameObject;
        Destroy(child);
        putStone(targetTile, -stateOfTile);
        stateOfTile *= -1;
    }

    private void changeTurn()
    {
        foreach(TileData tile in getValidMoves(-turn))
        {
            GameObject parentObject = tile.gameObject;
            drawBorder(parentObject);
        }
        turn *= -1;
    }

    private void drawBorder(GameObject tile)
    {
        GameObject borderObject = Instantiate((GameObject)Resources.Load("Prefabs/Border"), tile.transform);
    }

    //----- 指定のタイルが含まれる列のList生成 -----//
    private List<CircArray<TileData>> colListOfTile(TileData targetTile)
    {
        List<CircArray<TileData>> colList = new List<CircArray<TileData>>();
        foreach (CircArray<TileData> tiles in FindObjectOfType<ColList>().colList)
        {
            foreach (TileData tile in tiles)
            {
                if(tile == targetTile)
                {
                    colList.Add(tiles);
                }
            }
        }
        return colList;
    }

    //----- 指定の空白マスに指定の色の石が置けるか -----//
    private bool isValidMoves(TileData targetTile, int currentTurn)
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
            while(tiles[i+di].state == -currentTurn)
            {
                di++;
            }
            if(di > 1 && tiles[i+di].state == currentTurn)
            {
                return true;
            }
            di = 1;
            while( tiles[i-di].state == -currentTurn)
            {
                di++;
            }
            if(di > 1 && tiles[i-di].state == currentTurn)
            {
                return true;
            }
        }

        return false;
    }

    private List<TileData> getValidMoves(int currentTurn)
    {
        List<TileData> validMoves = new List<TileData>();
        foreach(TileData tile in tileDatas)
        {
            if(isValidMoves(tile, currentTurn))
            {
                validMoves.Add(tile);
            }
        }

        //NULLのタイルを除外
        TileData target_null = GameObject.Find("NULL").GetComponent<TileData>();
        if(validMoves.Contains(target_null)){
            validMoves.Remove(target_null);
        }

        return validMoves;
    }

    private void flip(TileData targetTile, int currentTurn)
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
            while(tiles[i+di].state == -currentTurn)
            {
                di++;
            }
            if(di > 1 && tiles[i+di].state == currentTurn)
            {
                while(di > 1)
                {
                    flipTiles.Add(tiles[i+(di-1)]);
                    di--;
                }
            }
            di = 1;
            while(tiles[i-di].state == -currentTurn)
            {
                di++;
            }
            if(di > 1 && tiles[i-di].state == currentTurn)
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
            changeStone(flipTile.gameObject);
        }
    }

    private void countStone(){
        countBlack = 0;
        countWhite = 0;
        for(int i = 0; i < tileDatas.Count; i++)
        {
            if(tileDatas[i].state == 1){
                countBlack++;
            }else if(tileDatas[i].state == -1){
                countWhite++;
            }
        }
        countBlackText.faceColor = new Color(0, 0, 0, 1);
        countBlackText.text = "BLACK : " + countBlack;
        countWhiteText.faceColor = new Color(255, 255, 255, 1);
        countWhiteText.text = "WHITE : " + countWhite;
    }

    private bool isGameOver(){
        if(getValidMoves(turn).Count == 0 && getValidMoves(-turn).Count == 0){
            return true;
        }
        return false;
    }

    private void result(){
        countStone();
        gameResult.faceColor = new Color(255,255,0,1);
        if(countBlack > countWhite){
            gameResult.text = "BLACK WINS!";
        }else if(countWhite > countBlack){
            gameResult.text = "WHITE WINS!";
        }else{
            gameResult.text = "DRAW!";
        }
    }

    private IEnumerator PerformCPUMove()
    {
        yield return new WaitForSeconds(1.0f); //待機時間

        List<TileData> validMoves = getValidMoves(CPUTurn);
        int randomIndex = Random.Range(0, validMoves.Count);
        TileData randomTile = validMoves[randomIndex];
        putStone(randomTile.gameObject, CPUTurn);
        flip(randomTile, CPUTurn);
        countStone();
        GameObject[] borders = GameObject.FindGameObjectsWithTag("Border");
        foreach (GameObject border in borders){
            Destroy(border);
        }

        changeTurn();
    }
}
