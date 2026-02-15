using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResultManager : MonoBehaviour
{
    public Text resultTimeText;

    void Start()
    {
        float lastTime = PlayerPrefs.GetFloat("LastRunTime", 0f);
        if (lastTime > 0)
        {
            resultTimeText.text = FormatTime(lastTime);
            UpdateRanking(lastTime);
        }
    }

    void UpdateRanking(float newTime)
    {
        List<float> times = new List<float>();

        // 1. 現在の1?10位を取得
        for (int i = 1; i <= 10; i++)
        {
            float savedTime = PlayerPrefs.GetFloat("Rank" + i, 9999f); // 初期値は十分に大きな値
            times.Add(savedTime);
        }

        // 2. 新しいタイムを追加してソート（昇順：早い順）
        times.Add(newTime);
        times.Sort();

        // 3. 上位10個を「Rank1」?「Rank10」として再保存
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