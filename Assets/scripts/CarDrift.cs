using UnityEngine;

public class CarDrift : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Drift Settings")]
    public float driftDrag = 2f;
    public float normalDrag = 0.05f;
    public float boostForce = 500f;

    [Header("Drift Logic")]
    [Tooltip("この角度（度）以上の横滑りがあればドリフトとみなす")]
    public float minDriftAngle = 10f;

    [Header("Audio Settings")]
    public AudioSource successAudio;  // 成功時の音
    public AudioSource failureAudio;  // 失敗時の音

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = normalDrag;
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

            // --- ここが修正ポイント ---

            // 1. 車の正面方向 (Forward) と 現在の進行方向 (Velocity) の角度差を計算
            float driftAngle = Vector3.Angle(transform.forward, rb.linearVelocity);

            // 2. 一定以上の角度差があり、かつ速度が出ている時だけブースト
            if (driftAngle > minDriftAngle && rb.linearVelocity.magnitude > 1f)
            {
                rb.AddForce(transform.forward * boostForce, ForceMode.Impulse);
                Debug.Log("Drift Boost! Angle: " + driftAngle);

                if (successAudio != null) successAudio.PlayOneShot(successAudio.clip);
            }
            else
            {
                Debug.Log("No Boost (Going Straight). Angle: " + driftAngle);
                if (failureAudio != null) failureAudio.PlayOneShot(failureAudio.clip);
            }
        }
    }
}