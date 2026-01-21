using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moveSpeed = 1500f;
    public float turnSpeed = 100f;
    public Transform spawnPoint;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1f, 0);

        if (spawnPoint != null)
        {
            // 位置と回転を開始地点に合わせる
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;

            // Rigidbodyを使っている場合は、念のため速度もリセット
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");   // W/S
        float turnInput = Input.GetAxis("Horizontal"); // A/D

        // ⭕ 正しい方法：車の「正面方向」に力を加える
        // transform.forward は、車がどこを向いていても「車の鼻先」の方向を指します
        rb.AddForce(transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime);

        // 回転も「車自身の軸」で回す
        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.fixedDeltaTime);
    }
}