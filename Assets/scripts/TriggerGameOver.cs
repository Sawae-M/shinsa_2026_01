using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerGameOver : MonoBehaviour
{
    // インスペクターから遷移先のシーン名を入力できるようにする
    [SerializeField] private string gameOverSceneName = "GameOver";

    // 物理的な衝突（跳ね返る設定）の場合
    private void OnCollisionEnter(Collision collision)
    {
        // 衝突した相手のタグが "Player" だった場合
        if (collision.gameObject.CompareTag("Player"))
        {
            LoadGameOverScene();
        }
    }

    // トリガー（すり抜ける設定）の場合
    private void OnTriggerEnter(Collider other)
    {
        // 接触した相手のタグが "Player" だった場合
        if (other.CompareTag("Player"))
        {
            LoadGameOverScene();
        }
    }

    private void LoadGameOverScene()
    {
        // 指定した名前のシーンを読み込む
        SceneManager.LoadScene(gameOverSceneName);
    }
}