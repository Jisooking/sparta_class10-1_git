using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PausePopup : MonoBehaviour
{
    public void OnClickBackButton()
    {
        GameManager.Instance.GameContinue();
        gameObject.SetActive(false);
    }

    public void OnClickMainButton()
    {
        SceneManager.LoadScene("StartScene");
        AudioManager.Instance.PlayNormalBGM();
    }

    public void OnClickExitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
