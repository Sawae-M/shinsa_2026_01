using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResultManager : MonoBehaviour
{
    public Text resultTimeText;
    public Text[] rankTexts;

    [Header("ランクイン演出")]
    public AudioSource audioSource; // 音を鳴らすスピーカー
    public AudioClip rankInSE;    // ランクインした時の特殊な効果音

    private int myRankIndex = -1;

    void Start()
    {
        float lastTime = PlayerPrefs.GetFloat("LastRunTime", 0f);
        if (lastTime > 0)
        {
            resultTimeText.text = FormatTime(lastTime);
            UpdateAndShowRanking(lastTime);
        }
    }

    void UpdateAndShowRanking(float newTime)
    {
        List<float> times = new List<float>();

        for (int i = 1; i <= 10; i++)
        {
            float savedTime = PlayerPrefs.GetFloat("Rank" + i, 9999f);
            times.Add(savedTime);
        }

        times.Add(newTime);
        times.Sort();

        myRankIndex = times.IndexOf(newTime);

        for (int i = 0; i < 10; i++)
        {
            float timeValue = times[i];
            PlayerPrefs.SetFloat("Rank" + (i + 1), timeValue);

            string rankLabel = "No." + (i + 1) + " ";
            if (timeValue >= 9999f)
            {
                rankTexts[i].text = rankLabel + "--:--.--";
            }
            else
            {
                rankTexts[i].text = rankLabel + FormatTime(timeValue);
            }
        }
        PlayerPrefs.Save();

        // --- ここから追加：ランクイン時の特殊演出 ---
        if (myRankIndex >= 0 && myRankIndex < 10)
        {
            // 1. 特殊な効果音を1回鳴らす
            if (audioSource != null && rankInSE != null)
            {
                audioSource.PlayOneShot(rankInSE);
            }

            // 2. 点滅を開始
            StartCoroutine(FlashMyRank(rankTexts[myRankIndex]));
        }
    }

    IEnumerator FlashMyRank(Text targetText)
    {
        while (true)
        {
            targetText.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            targetText.color = Color.yellow;
            yield return new WaitForSeconds(0.3f);
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 100) % 100);
        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }
}