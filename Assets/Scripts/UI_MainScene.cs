using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI_MainScene : MonoBehaviour
{
    public Text timeTxt;

    public GameObject ui_Hp;
    public Text roundTxt;

    public Text plusTimeText;

    public GameObject ui_SuccessPopup;
    public GameObject ui_FailPopup;
    public GameObject ui_DescriptionPopup;
    public GameObject ui_PausePopup;
    public GameObject ui_InfiniteModeClearPopup;

    public Slider timeSlider;

    private void Start()
    {
        GameManager.Instance.GameOverEvent -= PopupGameOver;
        GameManager.Instance.GameOverEvent += PopupGameOver;
        GameManager.Instance.GameClearEvent -= PopupGameClear;
        GameManager.Instance.GameClearEvent += PopupGameClear;
        GameManager.Instance.CardMatchEvent += PopupPlusTime;

        ui_DescriptionPopup.SetActive(true);
        ui_PausePopup.SetActive(false);
        ui_InfiniteModeClearPopup.SetActive(false);

        switch (Managers.Instance.gameType)
        {
            case GameLevel.Hidden:
                if (timeSlider != null)
                {
                    timeSlider.minValue = 0f;
                    timeSlider.maxValue = 1f;
                    timeSlider.value = 0f;
                    timeSlider.gameObject.SetActive(true);
                }
                break;
            case GameLevel.Infinite:
                PopupRound();
                break;
            case GameLevel.Zombie:
                timeTxt.gameObject.SetActive(true);
                ui_Hp.SetActive(true);
                break;
            default:
                if (timeSlider != null)
                {
                    timeSlider.minValue = 0f;
                    timeSlider.maxValue = 1f;
                    timeSlider.value = 0f;
                    timeSlider.gameObject.SetActive(true);
                }
                break;
        }
    }
    void Update()
    {

        switch (Managers.Instance.gameType)
        {
            case GameLevel.Hidden:
                timeTxt.text = GameManager.Instance._Time.ToString("N2");

                if (timeSlider != null && GameManager.Instance.maxtime > 0)
                {
                    float ratio = 1f - (GameManager.Instance._Time / GameManager.Instance.maxtime);
                    timeSlider.value = Mathf.Clamp01(ratio);
                }
                break;
            case GameLevel.Infinite:
                timeTxt.text = GameManager.Instance._Time.ToString("N2");
                roundTxt.text = $"{GameManager.Instance._Round} Round";
                break;
            case GameLevel.Zombie:
                timeTxt.text = GameManager.Instance._Time.ToString("N2");

                if (timeSlider != null && GameManager.Instance.maxtime > 0)
                {
                    float ratio = 1f - (GameManager.Instance._Time / GameManager.Instance.maxtime);
                    timeSlider.value = Mathf.Clamp01(ratio);
                }
                break;
            default:
                timeTxt.text = GameManager.Instance._Time.ToString("N2");

                if (timeSlider != null && GameManager.Instance.maxtime > 0)
                {
                    float ratio = 1f - (GameManager.Instance._Time / GameManager.Instance.maxtime);
                    timeSlider.value = Mathf.Clamp01(ratio);
                }
                break;
        }
    }

    void PopupGameOver()
    {
        if (Managers.Instance.gameType == GameLevel.Infinite)
        {
            ui_InfiniteModeClearPopup.SetActive(true);
        }
        else
        {
            ui_FailPopup.SetActive(true);
        }
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

    public void OnClickPauseButton()
    {
        ui_PausePopup.SetActive(true);
        GameManager.Instance.GamePause();
    }
}

