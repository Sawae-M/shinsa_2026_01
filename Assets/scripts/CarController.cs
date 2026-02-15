using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 5000f;
    public float turnSpeed = 100f;
    public float maxSpeed = 30f;

    [Header("浮き上がり防止設定")]
    public float downForce = 8000f;

    public Transform spawnPoint;
    private Rigidbody rb;

    // 変数名を smokeEffects に統一
    public ParticleSystem[] smokeEffects;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1f, 0);

        rb.useGravity = true;
        rb.drag = 0.5f;
        rb.angularDrag = 0.5f;

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

        // 1. 移動処理
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime, ForceMode.Force);
        }

        // 2. 回転処理
        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.fixedDeltaTime);

        // 3. ダウンフォース
        rb.AddForce(Vector3.down * downForce * Time.fixedDeltaTime, ForceMode.Force);

        // エフェクトの更新を呼び出す
        HandleEffects(moveInput);
    }

    // メソッドは FixedUpdate の「外」に定義します
    void HandleEffects(float moveInput)
    {
        if (smokeEffects == null) return;

        foreach (ParticleSystem smoke in smokeEffects)
        {
            if (smoke != null)
            {
                var emission = smoke.emission;
                // 入力がある（動いている）時だけ煙を出す
                emission.enabled = Mathf.Abs(moveInput) > 0.1f;
            }
        }
    }
}