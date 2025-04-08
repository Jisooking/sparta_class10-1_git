using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLevelTitle : MonoBehaviour
{
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.Instance.unlockNormal)
        {
            normalButton.interactable = true;
        }
        else 
        { 
            normalButton.interactable = false;
        }
        if (GameManager.Instance.unlockHard)
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
        GameManager.Instance.gameType = GameLevel.Easy;
        GameManager.Instance.SetTime();
        SceneManager.LoadScene("MainScene");

    }
    public void OnClickNormalButton()
    {
        GameManager.Instance.gameType = GameLevel.Normal;
        GameManager.Instance.SetTime();
        SceneManager.LoadScene("MainScene");
    }
    public void OnClickHardButton()
    {
        GameManager.Instance.gameType = GameLevel.Hard;
        GameManager.Instance.SetTime();
        SceneManager.LoadScene("MainScene");
    }
}
