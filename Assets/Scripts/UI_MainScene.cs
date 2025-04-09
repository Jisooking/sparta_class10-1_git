using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI_MainScene : MonoBehaviour
{
    public Text timeTxt;
    public GameObject EndPanel;
    public GameObject SuccessPanel;

    public GameObject hiddenPanel;
    public GameObject infinitePanel;
    public GameObject zombiePanel;

    private void Start()
    {
        GameManager.Instance.GameOverEvent -= PopupGameOver;
        GameManager.Instance.GameOverEvent += PopupGameOver;
        GameManager.Instance.GameClearEvent -= PopupGameClear;
        GameManager.Instance.GameClearEvent += PopupGameClear;

        //특별 모드의 경우, 설명 패널 팝업
        if (Managers.Instance.gameType == GameLevel.Hidden)
        {
            hiddenPanel.SetActive(true);
        }
        else if (Managers.Instance.gameType == GameLevel.Infinite)
        {
            infinitePanel.SetActive(true);
        }
        else if (Managers.Instance.gameType == GameLevel.Zombie)
        {
            zombiePanel.SetActive(true);
        }
    }

    void Update()
    {
        timeTxt.text = GameManager.Instance._Time.ToString("N2");
    }

    void PopupGameOver()
    {
        EndPanel.SetActive(true);
    }

    void PopupGameClear()
    {
        SuccessPanel.SetActive(true);
    }


    public void OnClickRetryButton()
    {
        SceneManager.LoadScene("MainScene");
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
