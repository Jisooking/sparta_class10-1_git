using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PausePopup : MonoBehaviour
{
    public void OnClickBackButton()
    {
        GameManager.Instance.GameStart();
        gameObject.SetActive(false);
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
