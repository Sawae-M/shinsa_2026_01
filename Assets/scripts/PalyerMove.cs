using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class UnityChanController_NoCamera : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float movePower = 30f;
    [SerializeField] private float rotationSpeed = 15.0f;

    private Rigidbody _rb;
    private Animator _animator;
    private Vector3 _moveDir;
    private float _inputMagnitude;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        // 物理設定
        _rb.freezeRotation = true;
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
        _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // 【重要】アニメーションによる移動制限をオフ
        _animator.applyRootMotion = false;
    }

    void Update()
    {
        // 1. WASD入力の取得
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 2. 世界の軸を基準にした移動方向の計算
        // Forward(Z軸)が前、Right(X軸)が横になります
        _moveDir = new Vector3(h, 0, v).normalized;

        // 入力の強さを計算（アニメーション用）
        _inputMagnitude = new Vector3(h, 0, v).magnitude;

        // 3. 移動している方向へキャラクターを向ける
        if (_moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(_moveDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                Time.deltaTime * rotationSpeed
            );
        }
    }

    void FixedUpdate()
    {
        // 4. 物理的な力を加えて移動
        if (_moveDir.sqrMagnitude > 0.01f)
        {
            _rb.AddForce(_moveDir * movePower, ForceMode.Acceleration);
        }

        // 5. アニメーションパラメーターの更新
        _animator.SetFloat("Speed", _inputMagnitude);
    }
}