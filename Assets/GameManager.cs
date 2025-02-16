using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText; // スコアを表示する UI
    public GameObject gameOverUI; // ゲームオーバー画面

    private float survivalTime = 0f; // 逃げた秒数

    void Update()
    {
        // ゲームオーバーでなければ時間をカウント
        if (gameOverUI.activeSelf == false)
        {
            survivalTime += Time.deltaTime;
            scoreText.text = "Score: " + survivalTime.ToString("F2") + "s"; // 小数2桁まで表示
        }
    }
}
