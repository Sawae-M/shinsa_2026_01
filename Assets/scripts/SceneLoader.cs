using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections; // コルーチンを使うために必要

public class SceneLoader : MonoBehaviour
{
    public string nextSceneName;      // 遷移先シーン名
    [SerializeField] private Image fadeImage; // フェード用のImageをアタッチ
    [SerializeField] private float fadeDuration = 3.0f; // フェードにかける時間

    public void OnClick()
    {
        // 直接シーンをロードせず、コルーチンを開始する
        StartCoroutine(FadeAndLoadScene());
    }

    private IEnumerator FadeAndLoadScene()
    {
        float timer = 0f;
        Color color = fadeImage.color;

        // 念のためフェード開始時にレイキャストをブロックする（連打防止）
        fadeImage.raycastTarget = true;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            // 3秒間かけてAlphaを0から1へ近づける
            color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadeImage.color = color;

            // 1フレーム待機
            yield return null;
        }

        // 完全に暗くなったらシーン移動
        SceneManager.LoadScene(nextSceneName);
    }
}