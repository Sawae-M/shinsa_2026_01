using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    public float gravityScale = 1.0f; // 重力の倍率
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // 標準の重力計算をオフにする
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        // 自分で下向きに力を加える
        // Physics.gravity は設定画面の重力値（通常 -9.81）
        Vector3 gravity = Physics.gravity * gravityScale;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}