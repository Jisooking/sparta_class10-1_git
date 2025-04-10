using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_GameModePopup : MonoBehaviour
{
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;

    void Start()
    {
        //난이도 활성화 유무에 따라 버튼 클릭 활성화/비활성화
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

    public void OnClickEasyButton()
    {
        Managers.Instance.gameType = GameLevel.Easy;
        Invoke("LoadMainScene", 1.0f);
    }

    public void OnClickNormalButton()
    {
        Managers.Instance.gameType = GameLevel.Normal;
        Invoke("LoadMainScene", 1.0f);
    }

    public void OnClickHardButton()
    {
        Managers.Instance.gameType = GameLevel.Hard;
        Invoke("LoadMainScene", 1.0f);
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    //돌아가기 버튼
    public void OnClickBackButton()
    {
        gameObject.SetActive(false);
    }
}
