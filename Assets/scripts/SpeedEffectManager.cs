using UnityEngine;
using UnityEngine.UI;

public class SpeedEffectManager : MonoBehaviour
{
    public Rigidbody carRigidbody; // 車のRigidbody
    public Image speedLineImage;   // 集中線のImage
    public float effectThreshold = 15f; // エフェクトが出始める速度
    public float maxEffectSpeed = 30f;  // 最大表示になる速度

    void Update()
    {
        if (carRigidbody == null || speedLineImage == null) return;

        // 現在の速度（時速/秒速）を取得
        float currentSpeed = carRigidbody.linearVelocity.magnitude;

        if (currentSpeed > effectThreshold)
        {
            // 速度に応じて透明度を 0?1 で計算
            float alpha = Mathf.InverseLerp(effectThreshold, maxEffectSpeed, currentSpeed);
            Color c = speedLineImage.color;
            c.a = alpha * 0.5f; // 最大でも半分くらいの透明度にする
            speedLineImage.color = c;
        }
        else
        {
            // 閾値以下なら非表示
            Color c = speedLineImage.color;
            c.a = 0;
            speedLineImage.color = c;
        }
    }
}