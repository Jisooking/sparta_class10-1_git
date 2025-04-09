using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_StartScene : MonoBehaviour
{
    public GameObject ui_GameModePopup;
    public GameObject ui_ScorePopup;
    public GameObject ui_SpecialModePopup;

    private void Start()
    {
        ui_GameModePopup.SetActive(false);
        ui_ScorePopup.SetActive(false);
        ui_SpecialModePopup.SetActive(false);
    }

    public void OnClickGameModeButton()
    {
        ui_GameModePopup.SetActive(true);
    }

    public void OnClickScoreButton()
    {
        ui_ScorePopup.SetActive(true);
    }

    public void OnClickSpecialModeButton()
    {
        ui_SpecialModePopup.SetActive(true);
    }

    public void OnClickExitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
