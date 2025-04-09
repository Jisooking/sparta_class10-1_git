using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI_MainScene : MonoBehaviour
{
    public Text timeTxt;

    public GameObject ui_SuccessPopup;
    public GameObject ui_FailPopup;
    public GameObject ui_DescriptionPopup;
    public GameObject ui_PausePopup;

    public Slider timeSlider;

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

        if (timeSlider != null)
        {
            timeSlider.minValue = 0f;
            timeSlider.maxValue = 1f;
            timeSlider.value = 0f;
        }
    }

    void Update()
    {
        timeTxt.text = GameManager.Instance._Time.ToString("N2");

        if (timeSlider != null && GameManager.maxtime > 0)
        {
            float ratio = 1f - (GameManager.time / GameManager.maxtime);
            timeSlider.value = Mathf.Clamp01(ratio);
        }
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
