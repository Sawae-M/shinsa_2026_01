using UnityEngine;

public class RecoadButton : MonoBehaviour
{
    // 表示・非表示を切り替えたいオブジェクト（ランキングパネルなど）
    public GameObject targetObject;

    // ボタンが押された時に実行するメソッド
    public void Toggle()
    {
        if (targetObject != null)
        {
            // 現在の状態が「アクティブ」なら「非アクティブ」に、逆なら「アクティブ」にする
            bool isActive = targetObject.activeSelf;
            targetObject.SetActive(!isActive);
        }
    }
}