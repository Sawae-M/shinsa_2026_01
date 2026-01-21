using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moveSpeed = 1500f;
    public float turnSpeed = 100f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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