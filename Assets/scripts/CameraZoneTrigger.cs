using UnityEngine;
using Unity.Cinemachine; 

public class CameraZoneTrigger : MonoBehaviour
{
    // 定点カメラをインスペクターでセット
    [SerializeField] private CinemachineCamera fixedCamera;

    // 優先度の設定
    [SerializeField] private int activePriority = 20;  // 区画にいる時の優先度
    [SerializeField] private int inactivePriority = 5; // 区画外の時の優先度

    private void OnTriggerEnter(Collider other)
    {
        // 車（Playerタグ）が入ったら定点カメラの優先度を上げる
        if (other.CompareTag("Player"))
        {
            fixedCamera.Priority = activePriority;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 車が出たら優先度を下げる（元のカメラが優先される）
        if (other.CompareTag("Player"))
        {
            fixedCamera.Priority = inactivePriority;
        }
    }
}