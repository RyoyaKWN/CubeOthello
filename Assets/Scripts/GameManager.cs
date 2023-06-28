using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject stoneWhite;
    [SerializeField] private GameObject stoneBlack;
    [SerializeField] private GameObject border;
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private TextMeshProUGUI countBlackText;
    [SerializeField] private TextMeshProUGUI countWhiteText;
    [SerializeField] private TextMeshProUGUI gameResult;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button returnButton;

    private float rotationSpeed = 100.0f;

    private GameObject clickedTile;
    private int turn;
    private int countBlack;
    private int countWhite;
    private TileData[] tileDatas;

    public ModeData modeData;
    private int CPUTurn;
    private Coroutine CPUMoveCoroutine;

    void Start()
    {
        tileDatas = FindObjectOfType<TileManager>().tileDatas;
        
        resetButton.onClick.AddListener(resetGame);
        returnButton.onClick.AddListener(returnToModeSelect);

        // 盤面の初期化
        turn = 1;
        putStone(GameObject.Find("Top2_2"), 1);
        putStone(GameObject.Find("Top3_3"), 1);
        putStone(GameObject.Find("Top2_3"), -1);
        putStone(GameObject.Find("Top3_2"), -1);
        foreach(TileData tile in getValidMoves(turn))
        {
            GameObject parentObject = tile.gameObject;
            drawBorder(parentObject);
        }

        // CPUの手番の取得
        CPUTurn = modeData.selectedMode;
        if(CPUTurn == 1){
            CPUMoveCoroutine = StartCoroutine(PerformCPUMove());
        }
    }

    void Update()
    {
        // クリック時の処理
        if (Input.GetMouseButtonDown(0)) {
            clickedTile = null;
            Vector3 clickPosition;
 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
 
            if (Physics.Raycast(ray, out hit)) {
                clickedTile = hit.collider.gameObject;
                clickPosition = hit.point;
                // 盤面上のマスがクリックされた場合
                if (clickedTile.CompareTag("Tile")){
                    //CPUの手番中は入力を受け付けない
                    if(turn == CPUTurn){
                        Debug.Log("Not Your Turn");
                        return;
                    }

                    if(isValidMoves(clickedTile.GetComponent<TileData>(), turn)){
                        putStone(clickedTile, turn);
                        flip(clickedTile.GetComponent<TileData>(), turn);
                        countStone();

                        if(getValidMoves(turn).Count == 0 && getValidMoves(-turn).Count == 0){ //勝敗判定
                            result();
                        }
                        else if(getValidMoves(-turn).Count > 0 && -turn == CPUTurn){ //CPUの手番
                            changeTurn();
                            CPUMoveCoroutine = StartCoroutine(PerformCPUMove());
                        }else if(getValidMoves(-turn).Count == 0){ //相手が石を置けないとき
                            changeTurn();    
                            changeTurn();
                        }
                        else{
                            changeTurn();
                        }
                    }
                }
            }else{
                clickedTile = null;
            }

 
        }

        // キューブ外でのドラッグ時の処理
        else if (Input.GetMouseButton(0))
        {
            if (clickedTile == null)
            {
                float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad * 5;
                float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad * 5;

                targetObject.transform.Rotate(Camera.main.transform.up, -rotX, Space.World);
                targetObject.transform.Rotate(Camera.main.transform.right, rotY, Space.World);
            }
        }

        // ターン表示
        if(turn == 1){
            turnText.faceColor = new Color(0, 0, 0, 1);
            turnText.text = "BLACK TURN";
        }else{
            turnText.faceColor = new Color(255, 255, 255, 1);
            turnText.text = "WHITE TURN";
        }
    }

    //----- 盤面を初期化 -----//
    private void resetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    //----- モード選択画面に移行 -----//
    private void returnToModeSelect()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ModeSelectScene");
    }

    //----- 石を置く -----//
    private void putStone(GameObject targetTile, int currentTurn)
    {
        TileData tileData = targetTile.GetComponent<TileData>();

        if (currentTurn == 1){
            Vector3 spawnPosition = targetTile.transform.position + new Vector3(0f, 0.5f, 0f);
            GameObject newObject = Instantiate(stoneBlack, spawnPosition, targetTile.transform.rotation, targetTile.transform);
            tileData.state = 1;
        }else{
            Vector3 spawnPosition = targetTile.transform.position + new Vector3(0f, 0.5f, 0f);
            GameObject newObject = Instantiate(stoneWhite, spawnPosition, targetTile.transform.rotation, targetTile.transform);
            tileData.state = -1;
        }
    }

    //----- ターン切り替え -----//
    private void changeTurn()
    {
        // 置けるマスのハイライトを再描画
        GameObject[] borders = GameObject.FindGameObjectsWithTag("Border");
        foreach(GameObject border in borders){
            Destroy(border);
        }
        foreach(TileData tile in getValidMoves(-turn))
        {
            GameObject parentObject = tile.gameObject;
            drawBorder(parentObject);
        }

        turn *= -1;
    }

    //----- マスをハイライト -----//
    private void drawBorder(GameObject tile)
    {
        GameObject borderObject = Instantiate(border, tile.transform);
    }

    //----- 指定のマスが含まれる列のList生成 -----//
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

    //----- 指定のマスに石が置けるかどうかの判定 -----//
    private bool isValidMoves(TileData targetTile, int currentTurn)
    {
        //すでに石が置かれていればFalse
        if (targetTile.state != 0)
        {
            return false;
        }

        List<CircArray<TileData>> colList = colListOfTile(targetTile);
        // すべての列（方向）で隣接してるマスを参照していく
        foreach (CircArray<TileData> tiles in colList)
        {
            int i = tiles.TakeWhile(tile => tile != targetTile).Count();
            if (i == tiles.size - 1)
            {
                continue;
            }
            int di = 1;
            while (tiles[i + di].state == -currentTurn)
            {
                di++;
            }
            if (di > 1 && tiles[i + di].state == currentTurn)
            {
                return true;
            }
            di = 1;
            while (tiles[i - di].state == -currentTurn)
            {
                di++;
            }
            if (di > 1 && tiles[i - di].state == currentTurn)
            {
                return true;
            }
        }

        return false;
    }

    //----- 石を置けるマスをすべて取得 -----//
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

    //----- 石をひっくり返す（色を変更） -----//
    private void changeStone(GameObject targetTile)
    {
        int stateOfTile = targetTile.GetComponent<TileData>().state;
        GameObject child = targetTile.transform.GetChild(0).gameObject;
        Destroy(child);
        putStone(targetTile, -stateOfTile);
        stateOfTile *= -1;
    }

    //----- 石を置いた際のひっくり返し -----//
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

    //----- 石の個数を数える -----//
    private void countStone(){
        countBlack = 0;
        countWhite = 0;
        for(int i = 0; i < tileDatas.Length; i++)
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

    //----- リザルト表示 -----//
    private void result(){
        gameResult.faceColor = new Color(255,255,0,1);
        if(countBlack > countWhite){ //黒の勝利
            gameResult.text = "BLACK WINS!";
        }else if(countWhite > countBlack){ //白の勝利
            gameResult.text = "WHITE WINS!";
        }else{ //引き分け
            gameResult.text = "DRAW!";
        }
    }

    //----- CPU（ランダム） -----//
    private IEnumerator PerformCPUMove()
    {
        yield return new WaitForSeconds(1.0f); //待機時間

        List<TileData> validMoves = getValidMoves(CPUTurn);
        int randomIndex = Random.Range(0, validMoves.Count);
        TileData randomTile = validMoves[randomIndex];
        putStone(randomTile.gameObject, CPUTurn);
        flip(randomTile, CPUTurn);
        countStone();
        changeTurn();
    }
}


