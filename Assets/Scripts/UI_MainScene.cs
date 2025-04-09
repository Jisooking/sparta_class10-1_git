using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI_MainScene : MonoBehaviour
{
    public Text timeTxt;
    public Text zombieCountText;

    public GameObject ui_SuccessPopup;
    public GameObject ui_FailPopup;
    public GameObject ui_DescriptionPopup;
    public GameObject ui_PausePopup;

    private void Start()
    {
        GameManager.Instance.GameOverEvent -= PopupGameOver;
        GameManager.Instance.GameOverEvent += PopupGameOver;
        GameManager.Instance.GameClearEvent -= PopupGameClear;
        GameManager.Instance.GameClearEvent += PopupGameClear;

        ui_SuccessPopup.SetActive(false);
        ui_FailPopup.SetActive(false);
        ui_DescriptionPopup.SetActive(true);
        ui_PausePopup.SetActive(false);
    }

    void Update()
    {
        timeTxt.text = GameManager.Instance._Time.ToString("N2");
        zombieCountText.text = GameManager.Instance.zombieCount.ToString();
    }

    void PopupGameOver()
    {
        ui_FailPopup.SetActive(true);
    }

    void PopupGameClear()
    {
        ui_SuccessPopup.SetActive(true);
    }

    public void OnClickPauseButton()
    {
        GameManager.Instance.GameStop();
        ui_PausePopup.SetActive(true);
    }
}
