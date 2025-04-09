using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Button : MonoBehaviour
{
    public void OnClickContinueButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickMainButton()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void OnClickExitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
