using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 5000f;  // 動かない場合はここを大きく
    public float turnSpeed = 100f;
    public float maxSpeed = 30f;

    [Header("浮き上がり防止設定")]
    public float downForce = 8000f; // 地面に吸い付かせるための強い力

    public Transform spawnPoint;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 重心を低くして安定させる
        rb.centerOfMass = new Vector3(0, -1f, 0);

        // Rigidbodyの基本設定をスクリプトから固定
        rb.useGravity = true;
        rb.drag = 0.5f;           // 空中で加速しすぎないための抵抗
        rb.angularDrag = 0.5f;    // 回転が止まりやすくする

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
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // 1. 移動処理（接地判定なしで常に実行）
        // 最高速度を超えていない時だけ力を加える
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime, ForceMode.Force);
        }

        // 2. 回転処理
        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.fixedDeltaTime);

        // 3. ダウンフォース
        // 空を飛ばないように、常に強力な下向きの力を加え続ける
        rb.AddForce(Vector3.down * downForce * Time.fixedDeltaTime, ForceMode.Force);
    }
}