using UnityEngine;
using UnityEngine.UI;

public class RankingDisplay : MonoBehaviour
{
    [Header("10個のText（No.1用?No.10用）を順番にアタッチ")]
    public Text[] rankTexts;

    void OnEnable()
    {
        ShowRanking();
    }

    public void ShowRanking()
    {
        for (int i = 0; i < rankTexts.Length; i++)
        {
            // Rank1, Rank2... という名前で保存されている値を取得
            float time = PlayerPrefs.GetFloat("Rank" + (i + 1), 9999f);

            // 順位ラベルを作成（No.1, No.2...）
            string rankLabel = "No." + (i + 1) + " ";

            if (time >= 9999f || time <= 0f)
            {
                rankTexts[i].text = rankLabel + "--:--.--";
            }
            else
            {
                rankTexts[i].text = rankLabel + FormatTime(time);
            }
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