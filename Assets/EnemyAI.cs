using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // プレイヤーを追跡する
    private NavMeshAgent agent;
    private bool isChasing = false;

    public GameObject gameOverUI;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
       if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // エネミーがプレイヤーにぶつかったら
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOverUI.SetActive(true); // ゲームオーバー UI を表示
        Time.timeScale = 0; // ゲームを停止
    }
}
