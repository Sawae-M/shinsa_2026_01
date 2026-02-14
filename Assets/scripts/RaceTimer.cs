using UnityEngine;
using TMPro;
using System.Collections;

public class RaceTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI timerText;

    // 車の制御スクリプト（例：PrometeoCarControllerなど）をここに参照
    // ここでは単純なGameObjectの有効/無効で制御する例にします
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
            countdownText.text = count.ToString("0");
            yield return new WaitForSeconds(1f);
            count--;
        }

        countdownText.text = "GO!!";

        // 車を動かせるようにする
        if (carControllerScript != null) carControllerScript.enabled = true;
        isRacing = true;

        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isRacing)
        {
            raceTime += Time.deltaTime;
            // 00:00.00 形式で表示
            timerText.text = string.Format("{0:00}:{1:00}.{2:00}",
                Mathf.FloorToInt(raceTime / 60),
                Mathf.FloorToInt(raceTime % 60),
                Mathf.FloorToInt((raceTime * 100) % 100));
        }
    }
}