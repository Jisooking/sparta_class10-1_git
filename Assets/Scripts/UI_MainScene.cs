using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI_MainScene : MonoBehaviour
{
    public Text timeTxt;
    public GameObject EndPanel;
    public GameObject SuccessPanel;

    private void Start()
    {
        GameManager.Instance.GameOverEvent -= PopupGameOver;
        GameManager.Instance.GameOverEvent += PopupGameOver;
        GameManager.Instance.GameClearEvent -= PopupGameClear;
        GameManager.Instance.GameClearEvent += PopupGameClear;
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
        EndPanel.SetActive(true);
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
