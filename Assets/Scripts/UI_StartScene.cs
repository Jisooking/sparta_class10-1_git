using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_StartScene : MonoBehaviour
{
    public GameObject GameLevelTitle;
    public GameObject ui_ScorePopup;
<<<<<<< HEAD
<<<<<<< HEAD
    public GameObject ui_SpecialModePopup;

=======

    public Button easyButton;
    public Button normalButton;
    public Button hardButton;
=======
    public GameObject SpecialLevelTitle;

    public Button easyButton;
    public Button normalButton;
    public Button hardButton;
    public Button ZombieButton;
    public Button InfiniteButton;
>>>>>>> parent of 9beb8db (Merge branch 'Develop' into juho_branch)

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
<<<<<<< HEAD
>>>>>>> parent of 2daeabb (Feat feat 스페셜 모드 선택 UI > 연결)
=======
>>>>>>> parent of 9beb8db (Merge branch 'Develop' into juho_branch)

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

<<<<<<< HEAD
<<<<<<< HEAD
    public void PopupSpeicalMode()
=======
    public void LoadSpeicalLevel()
>>>>>>> parent of 9beb8db (Merge branch 'Develop' into juho_branch)
    {
        SpecialLevelTitle.SetActive(true);
    }

<<<<<<< HEAD
=======
=======
>>>>>>> parent of 9beb8db (Merge branch 'Develop' into juho_branch)
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
<<<<<<< HEAD

=======
    public void OnClickZombieButton()
    {
        Managers.Instance.gameType = GameLevel.Zombie;
        LoadMainScene();
    }
    public void OnClickInfiniteButton()
    {
        Managers.Instance.gameType = GameLevel.Infinite;
        LoadMainScene();

    }
>>>>>>> parent of 9beb8db (Merge branch 'Develop' into juho_branch)
    void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
<<<<<<< HEAD
>>>>>>> parent of 2daeabb (Feat feat 스페셜 모드 선택 UI > 연결)
=======
>>>>>>> parent of 9beb8db (Merge branch 'Develop' into juho_branch)
}
