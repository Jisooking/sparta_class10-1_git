using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_StartScene : MonoBehaviour
{
    public GameObject ui_GameModePopup;
    public GameObject ui_ScorePopup;
    public GameObject ui_SpecialModePopup;


    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void PopupGameMode()
    {
        ui_GameModePopup.SetActive(true);
    }

    public void PopupScore()
    {
        ui_ScorePopup.SetActive(true);
    }

    public void PopupSpeicalMode()
    {
        ui_SpecialModePopup.SetActive(true);
    }

}
