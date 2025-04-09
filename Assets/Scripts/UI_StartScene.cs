using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_StartScene : MonoBehaviour
{
    public GameObject ui_GameModePopup;
    public GameObject ui_ScorePopup;
<<<<<<< HEAD
    public GameObject ui_SpecialModePopup;

=======

    public Button easyButton;
    public Button normalButton;
    public Button hardButton;

    void Start()
    {
        if (Managers.Instance.unlockNormal)
        {
            normalButton.interactable = true;
        }
        else
        {
            normalButton.interactable = false;
        }
        if (Managers.Instance.unlockHard)
        {
            hardButton.interactable = true;
        }
        else
        {
            hardButton.interactable = false;
        }
    }
>>>>>>> parent of 2daeabb (Feat feat 스페셜 모드 선택 UI > 연결)

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

<<<<<<< HEAD
    public void PopupSpeicalMode()
    {
        ui_SpecialModePopup.SetActive(true);
    }

=======
    public void OnClickEasyButton()
    {
        Managers.Instance.gameType = GameLevel.Easy;
        LoadMainScene();

    }
    public void OnClickNormalButton()
    {
        Managers.Instance.gameType = GameLevel.Normal;
        LoadMainScene();
    }
    public void OnClickHardButton()
    {
        Managers.Instance.gameType = GameLevel.Hard;
        LoadMainScene();
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
>>>>>>> parent of 2daeabb (Feat feat 스페셜 모드 선택 UI > 연결)
}
