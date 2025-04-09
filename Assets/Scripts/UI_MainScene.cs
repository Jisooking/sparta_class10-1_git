using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI_MainScene : MonoBehaviour
{
    public Text timeTxt;
    public Text zombieCountText;

    public GameObject ui_Hp;
    public Text roundTxt;

    public Text plusTimeText;

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
        GameManager.Instance.CardMatchEvent += PopupPlusTime;

        ui_DescriptionPopup.SetActive(true);
        ui_PausePopup.SetActive(false);

        if (Managers.Instance.gameType == GameLevel.Zombie)
            ui_Hp.SetActive(true);

        if (Managers.Instance.gameType == GameLevel.Infinite)
        {
            PopupRound();
        }
    }

    void Update()
    {
        timeTxt.text = GameManager.Instance._Time.ToString("N2");
        roundTxt.text = GameManager.Instance._Round.ToString();
    }

    void PopupGameOver()
    {
        ui_FailPopup.SetActive(true);
    }

    void PopupGameClear()
    {
        ui_SuccessPopup.SetActive(true);
    }

    void PopupRound()
    {
        roundTxt.gameObject.SetActive(true);
    }

    void PopupPlusTime()
    {
        plusTimeText.gameObject.SetActive(false);
        plusTimeText.gameObject.SetActive(true);
    }
}
