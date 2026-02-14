using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshProを使用する場合
using System.Collections;

public class GoalManager : MonoBehaviour
{
    [Header("UI設定")]
    public GameObject goalUIPanel; // 「ゴール」と表示されるパネル/テキスト

    [Header("オーディオ設定")]
    public AudioSource audioSource;
    public AudioClip goalSE;

    [Header("シーン設定")]
    public string resultSceneName = "ResultScene"; // リザルトシーンの名前

    private bool isReached = false;

    private void OnTriggerEnter(Collider foreign)
    {
        // プレイヤー（車）に "Player" タグをつけておいてください
        if (foreign.CompareTag("Player") && !isReached)
        {
            isReached = true;
            StartCoroutine(GoalSequence());
        }
    }

    IEnumerator GoalSequence()
    {
        // 1. 効果音を鳴らす
        if (audioSource && goalSE)
        {
            audioSource.PlayOneShot(goalSE);
        }

        // 2. ゴールUIを表示
        if (goalUIPanel != null)
        {
            goalUIPanel.SetActive(true);
        }

        // 3. 5秒待機
        yield return new WaitForSeconds(5f);

        // 4. リザルトシーンへ移行
        SceneManager.LoadScene(resultSceneName);
    }
}