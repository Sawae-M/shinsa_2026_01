using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 5000f;  // 数値を大きめに調整
    public float turnSpeed = 100f;
    public float maxSpeed = 20f;
    public float downForce = 5000f; // 浮き上がり防止に強めに設定

    [Header("接地設定")]
    public float rayDistance = 1.5f; // 車高に合わせて調整
    public Vector3 rayOffset = new Vector3(0.5f, 0, 0.5f); // センサーを四隅に広げる幅

    public Transform spawnPoint;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1f, 0); // 重心を低く保つ

        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        // 1. 4隅のセンサーで接地判定を強化
        CheckGroundedExtended();

        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // 2. 移動処理（地面にいる時だけ加速する！）
        if (isGrounded)
        {
            if (rb.linearVelocity.magnitude < maxSpeed)
            {
                rb.AddForce(transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime, ForceMode.Force);
            }
        }

        // 3. 回転処理
        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.fixedDeltaTime);

        // 4. 強力なダウンフォース（空中にいても常に下へ引っ張る）
        rb.AddForce(Vector3.down * downForce * Time.fixedDeltaTime, ForceMode.Force);
    }

    // 4つのポイントで地面をチェックする
    void CheckGroundedExtended()
    {
        isGrounded = false;
        // チェックする4つの位置（前右、前左、後右、後左）
        Vector3[] offsets = {
            new Vector3(rayOffset.x, 0, rayOffset.z),
            new Vector3(-rayOffset.x, 0, rayOffset.z),
            new Vector3(rayOffset.x, 0, -rayOffset.z),
            new Vector3(-rayOffset.x, 0, -rayOffset.z)
        };

        foreach (var offset in offsets)
        {
            Vector3 rayStart = transform.TransformPoint(offset + Vector3.up * 0.1f);
            if (Physics.Raycast(rayStart, Vector3.down, rayDistance))
            {
                isGrounded = true;
                break; // どこか1つでも当たれば接地とみなす
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3[] offsets = {
            new Vector3(rayOffset.x, 0, rayOffset.z),
            new Vector3(-rayOffset.x, 0, rayOffset.z),
            new Vector3(rayOffset.x, 0, -rayOffset.z),
            new Vector3(-rayOffset.x, 0, -rayOffset.z)
        };
        foreach (var offset in offsets)
        {
            Vector3 rayStart = transform.TransformPoint(offset + Vector3.up * 0.1f);
            Gizmos.DrawLine(rayStart, rayStart + Vector3.down * rayDistance);
        }
    }
}