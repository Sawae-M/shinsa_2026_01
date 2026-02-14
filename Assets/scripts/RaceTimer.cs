using UnityEngine;
using UnityEngine.UI; // Textを使用するために必要
using System.Collections;

public class RaceTimer : MonoBehaviour
{
    [Header("UI参照")]
    public Text countdownText;
    public Text timerText;

    [Header("走行制御")]
    public MonoBehaviour carControllerScript;

    private float raceTime = 0f;
    private bool isRacing = false;

    void Start()
    {
        // 最初は車を動かさない
        if (carControllerScript != null) carControllerScript.enabled = false;

        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        float count = 5f;
        while (count > 0)
        {
            if (countdownText != null) countdownText.text = count.ToString("0");
            yield return new WaitForSeconds(1f);
            count--;
        }

        if (countdownText != null) countdownText.text = "GO!!";

        // 車を動かせるようにする
        if (carControllerScript != null) carControllerScript.enabled = true;
        isRacing = true;

        yield return new WaitForSeconds(1f);
        if (countdownText != null) countdownText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isRacing)
        {
            raceTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(raceTime / 60);
            int seconds = Mathf.FloorToInt(raceTime % 60);
            int milliseconds = Mathf.FloorToInt((raceTime * 100) % 100);
            timerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
        }
    }

    // ゴール時に外部から呼ぶためのメソッド
    public void StopTimer()
    {
        isRacing = false;
    }
}