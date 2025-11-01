using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScene : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        if (SceneController.Instance != null)
        {
            SceneController.Instance.LoadSceneWithFade("GameScene");
        }
        else
        {
            Debug.LogError("SceneController.Instance is null!");
            SceneManager.LoadScene("GameScene");
        }
    }
}