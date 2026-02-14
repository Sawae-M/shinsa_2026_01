using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResultManager : MonoBehaviour
{
    public Text resultTimeText; // 今回のタイムを表示するText

    void Start()
    {
        // 1. 今回のタイムを取得して表示
        float lastTime = PlayerPrefs.GetFloat("LastRunTime", 0f);
        resultTimeText.text = FormatTime(lastTime);

        // 2. ランキングを更新
        UpdateRanking(lastTime);
    }

    void UpdateRanking(float newTime)
    {
        List<float> times = new List<float>();

        // 保存されている1?10位を読み込む
        for (int i = 1; i <= 10; i++)
        {
            float savedTime = PlayerPrefs.GetFloat("Rank" + i, 999.99f); // 初期値は遅いタイム
            times.Add(savedTime);
        }

        // 新しいタイムを追加してソート（速い順）
        times.Add(newTime);
        times.Sort();

        // 上位10位までを保存し直す
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetFloat("Rank" + (i + 1), times[i]);
        }
        PlayerPrefs.Save();
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 100) % 100);
        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }
}