using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInEffect : MonoBehaviour
{
    [SerializeField] private Image fadeImage; // フェード用の黒いImage
    [SerializeField] private float fadeDuration = 3.0f; // 3秒かけてフェード

    void Start()
    {
        // 1. まず画面を真っ黒にする（Alpha = 1）
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        // 2. フェードイン開始
        StartCoroutine(DoFadeIn());
    }

    private IEnumerator DoFadeIn()
    {
        float timer = 0f;
        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            // 3秒間かけてAlphaを1から0へ近づける（明るくする）
            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadeImage.color = color;

            yield return null;
        }

        // 3. 最後に完全に透明にする
        color.a = 0f;
        fadeImage.color = color;

        // 4. フェードが終わったら、下のボタンなどが押せるように画像を無効化する
        fadeImage.gameObject.SetActive(false);
    }
}