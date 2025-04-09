using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_StartScene : MonoBehaviour
{
    public GameObject GameLevelTitle;
    public GameObject ui_ScorePopup;
    public GameObject SpecialLevelTitle;

    public Button easyButton;
    public Button normalButton;
    public Button hardButton;

    public Button zombieButton;
    public Button infinitiButton;


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

    public void LoadSpeicalLevel()
    {
        SpecialLevelTitle.SetActive(true);
    }

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

    public void OnclickZombieButton()
    {
        Debug.Log("좀비모드 입장");
    }

    public void OnclickInfinitiButton()
    {
        Debug.Log("무한모드 입장");
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
