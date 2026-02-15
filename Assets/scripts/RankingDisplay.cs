using UnityEngine;
using UnityEngine.UI;

public class RankingDisplay : MonoBehaviour
{
    [Header("1ˆÊ?10ˆÊ‚ÌText‚ğ‡”Ô‚É“ü‚ê‚Ä‚­‚¾‚³‚¢")]
    public Text[] rankTexts;

    void OnEnable() // ‰æ–Ê‚ªŠJ‚©‚ê‚é‚½‚Ñ‚ÉXV
    {
        ShowRanking();
    }

    public void ShowRanking()
    {
        for (int i = 0; i < rankTexts.Length; i++)
        {
            float time = PlayerPrefs.GetFloat("Rank" + (i + 1), 0f);

            if (time <= 0f || time >= 999f)
            {
                rankTexts[i].text = "No." + (i + 1) + " --:--.--";
            }
            else
            {
                rankTexts[i].text = "No. " + (i + 1) +  FormatTime(time);
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