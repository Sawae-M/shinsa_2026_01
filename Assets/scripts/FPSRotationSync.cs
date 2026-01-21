using UnityEngine;
using Unity.Cinemachine; // Unity 6のネームスペース

public class FPSRotationSync : MonoBehaviour
{
    public CinemachineCamera vcam;

    void Update()
    {
        if (vcam != null)
        {
            // カメラの現在のY軸回転（左右）を取得して、プレイヤー本体の回転に適用
            float yRotation = vcam.transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}