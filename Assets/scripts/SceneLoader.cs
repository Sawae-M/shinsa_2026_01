using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string nextSceneName;      // 遷移先シーン名
    public void OnClick()
    {
        SceneManager.LoadScene(nextSceneName);// シーン切り替え
    }
}
