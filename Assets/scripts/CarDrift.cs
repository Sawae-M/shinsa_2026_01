using UnityEngine;

public class CarDrift : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Drift Settings")]
    public float driftDrag = 2f;
    public float normalDrag = 0.05f;
    public float boostForce = 500f;

    [Header("Drift Logic")]
    public float minDriftAngle = 10f;

    [Header("Audio Settings")]
    public AudioSource successAudio;
    public AudioSource failureAudio;

    [Header("Effect Settings")]
    // 追加：青い炎のエフェクト（複数対応）
    public ParticleSystem[] boostFlames;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = normalDrag;

        // 開始時はエフェクトを止めておく
        StopBoostFlames();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearDamping = driftDrag;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.linearDamping = normalDrag;

            float driftAngle = Vector3.Angle(transform.forward, rb.linearVelocity);

            if (driftAngle > minDriftAngle && rb.linearVelocity.magnitude > 1f)
            {
                // ドリフト成功
                rb.AddForce(transform.forward * boostForce, ForceMode.Impulse);

                // 追加：ブーストエフェクト再生
                PlayBoostFlames();

                if (successAudio != null) successAudio.PlayOneShot(successAudio.clip);
            }
            else
            {
                if (failureAudio != null) failureAudio.PlayOneShot(failureAudio.clip);
            }
        }
    }

    // エフェクトを一斉に再生するメソッド
    void PlayBoostFlames()
    {
        foreach (var flame in boostFlames)
        {
            if (flame != null)
            {
                flame.Play(); // 再生開始
            }
        }
    }

    // エフェクトを強制停止する（必要ならStart時などに呼ぶ）
    void StopBoostFlames()
    {
        foreach (var flame in boostFlames)
        {
            if (flame != null) flame.Stop();
        }
    }
}