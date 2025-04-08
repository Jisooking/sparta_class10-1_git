using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StartScene : MonoBehaviour
{
    public GameObject GameLevelTitle;
    public GameObject ui_ScorePopup;

    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadLevel()
    {
        GameLevelTitle.SetActive(true);
    }

    public void PopupScore()
    {
        ui_ScorePopup.SetActive(true);
    }
}
