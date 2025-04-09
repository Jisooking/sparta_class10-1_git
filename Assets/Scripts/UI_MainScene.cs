using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI_MainScene : MonoBehaviour
{
    public Text timeTxt;
    public Text roundTxt;

    public GameObject ui_SuccessPopup;
    public GameObject ui_FailPopup;
    public GameObject ui_DescriptionPopup;

    private void Start()
    {
        GameManager.Instance.GameOverEvent -= PopupGameOver;
        GameManager.Instance.GameOverEvent += PopupGameOver;
        GameManager.Instance.GameClearEvent -= PopupGameClear;
        GameManager.Instance.GameClearEvent += PopupGameClear;

        ui_DescriptionPopup.SetActive(true);

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
}
