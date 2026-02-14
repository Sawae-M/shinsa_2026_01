using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Textを使用するために必要
using System.Collections;

public class GoalManager : MonoBehaviour
{
    [Header("UI設定")]
    public GameObject goalUIPanel; // 「ゴール」と表示されるパネルやTextの親
    public Text goalText;          // 直接Textを操作する場合

    [Header("オーディオ設定")]
    public AudioSource audioSource;
    public AudioClip goalSE;

    [Header("シーン設定")]
    public string resultSceneName = "ResultScene";

    private bool isReached = false;

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーに "Player" タグを設定
        if (other.CompareTag("Player") && !isReached)
        {
            isReached = true;
            StartCoroutine(GoalSequence());
        }
    }

    IEnumerator GoalSequence()
    {
        // 1. 効果音
        if (audioSource && goalSE) audioSource.PlayOneShot(goalSE);

        // 2. UI表示
        if (goalUIPanel != null) goalUIPanel.SetActive(true);
        if (goalText != null) goalText.text = "ゴール！";

        // タイマーを止める（後述のRaceTimerを止める場合）
        RaceTimer timer = FindObjectOfType<RaceTimer>();
        if (timer != null) timer.StopTimer();

        // 3. 5秒待機
        yield return new WaitForSeconds(5f);

        // 4. シーン遷移
        SceneManager.LoadScene(resultSceneName);
    }
}