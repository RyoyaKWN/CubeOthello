using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ModeSetManager : MonoBehaviour
{
    public TMP_Dropdown modeDropdown;
    public Button startButton;

    // public GameMode selectedMode;
    private int[] modeValues = {0, -1, 1};
    public ModeData modeData;

    private void Start()
    {
        // スタートボタンのクリックイベントを設定
        startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        // ドロップダウンで選択されたモードを取得
        int selectedIndex = modeDropdown.value;

        //モードに対応する値を取得
        modeData.selectedMode = modeValues[selectedIndex];

        // GameManagerスクリプトがアタッチされたオブジェクトを検索
        // GameManager gameManager = FindObjectOfType<GameManager>();

        // // オブジェクトが見つかった場合、モード選択結果を格納する変数を更新
        // if (gameManager != null)
        // {
        //     gameManager.CPUTurn = selectedMode;
        // }

        // シーンを移動
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}