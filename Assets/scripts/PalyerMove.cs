using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMove : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float movePower = 30f;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float rotationSpeed = 15.0f;
    [SerializeField] private float downForce = 20f; // 坂で浮かないための下向きの力

    [Header("接地設定")]
    [SerializeField] private float rayDistance = 1.0f; // 地面を検知する距離
    [SerializeField] private LayerMask groundLayer;   // 地面のレイヤーを指定

    private Rigidbody _rb;
    private Animator _animator;
    private Vector3 _moveDir;
    private bool _isGrounded;
    private Vector3 _groundNormal;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _rb.freezeRotation = true;
        _rb.useGravity = true; // 基本の重力は使用
        _animator.applyRootMotion = false;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 入力方向
        Vector3 inputDir = new Vector3(h, 0, v).normalized;

        // 接地判定と地面の角度（法線）を取得
        CheckGrounded();

        // 地面の傾斜に合わせた移動方向の計算
        _moveDir = Vector3.ProjectOnPlane(inputDir, _groundNormal).normalized;

        if (inputDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(inputDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotationSpeed);
        }
    }

    void FixedUpdate()
    {
        // 1. 地面に付いている時だけ加速できる
        if (_isGrounded && _moveDir.sqrMagnitude > 0.01f)
        {
            if (_rb.linearVelocity.magnitude < maxSpeed)
            {
                _rb.AddForce(_moveDir * movePower, ForceMode.Acceleration);
            }
        }

        // 2. 常に強力な下向きの力を加えて地面に吸い付かせる
        // （空中でも重力を助けて早く着地させる）
        _rb.AddForce(Vector3.down * downForce, ForceMode.Acceleration);

        // アニメーション更新
        float currentSpeedRatio = _rb.linearVelocity.magnitude / maxSpeed;
        _animator.SetFloat("Speed", currentSpeedRatio);
    }

    void CheckGrounded()
    {
        RaycastHit hit;
        // 足元にレイを飛ばして地面があるか、その角度はどうなっているか確認
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, rayDistance, groundLayer))
        {
            _isGrounded = true;
            _groundNormal = hit.normal; // 地面の垂直ベクトルの取得
        }
        else
        {
            _isGrounded = false;
            _groundNormal = Vector3.up; // 空中では真上が基準
        }
    }

    // デバッグ用：地面検知の線をシーンビューに表示
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.down * (rayDistance - 0.1f));
    }
}