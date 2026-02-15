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
    public ParticleSystem[] smokeEffects;

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1f, 0);

        rb.useGravity = true;
        // Dragを少し上げると挙動が安定します
        rb.linearDamping = 1.0f;
        rb.angularDamping = 1.0f;

        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void Update()
    {
        // 入力の取得は Update で行う（ガクつき防止に重要）
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        // 1. 移動処理（滑らかな加速）
        ApplyMovement();

        // 2. 回転処理
        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.fixedDeltaTime);

        // 3. ダウンフォース（少しマイルドにする、または地面との距離で調整）
        rb.AddForce(Vector3.down * downForce * Time.fixedDeltaTime, ForceMode.Force);

        // エフェクトの更新
        HandleEffects(moveInput);
    }

    void ApplyMovement()
    {
        // 現在の進行方向への速度を計算
        float currentForwardSpeed = Vector3.Dot(rb.linearVelocity, transform.forward);

        // 最高速度に近づいたら加える力を徐々に弱めることでガクつきを抑える
        if (Mathf.Abs(currentForwardSpeed) < maxSpeed)
        {
            rb.AddForce(transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime, ForceMode.Force);
        }
    }

    void HandleEffects(float input)
    {
        if (smokeEffects == null) return;

        foreach (ParticleSystem smoke in smokeEffects)
        {
            if (smoke != null)
            {
                var emission = smoke.emission;
                // 入力の絶対値で判定
                emission.enabled = Mathf.Abs(input) > 0.1f;
            }
        }
    }
}