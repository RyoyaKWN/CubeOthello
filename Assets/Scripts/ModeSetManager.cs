using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ModeSetManager : MonoBehaviour
{
    [SerializeField]
    public Button twoPlayerButton; // 2プレイヤーモードを選択するボタン
    public Button playerVsCPUButton; // 1プレイヤー(先攻)+CPUモードを選択するボタン
    public Button CPUVsPlayerButton; // 1プレイヤー(後攻)+CPUモードを選択するボタン

    private void Start()
    {
        // ボタンにクリック時のイベントを追加
        twoPlayerButton.onClick.AddListener(SelectTwoPlayersMode);
        playerVsCPUButton.onClick.AddListener(SelectPlayerVsCPUMode);
        CPUVsPlayerButton.onClick.AddListener(SelectCPUVsPlayerMode);
    }

    public void SelectTwoPlayersMode()
    {
        // 2プレイヤーモードの処理を記述する
        // 例: ゲームのシーンをロードして2人プレイ用のゲームを開始する
        UnityEngine.SceneManagement.SceneManager.LoadScene("TwoPlayersScene");
    }

    private void SelectPlayerVsCPUMode()
    {
        // 1プレイヤー(先攻)+CPUモードの処理を記述する
        // SceneManager.LoadScene("PlayerVsCPUScene");
    }

    private void SelectCPUVsPlayerMode()
    {
        // 1プレイヤー(後攻)+CPUモードの処理を記述する
        // SceneManager.LoadScene("CPUVsPlayerScene");
    }
}
