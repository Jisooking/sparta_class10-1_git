using UnityEngine;
using UnityEngine.UI;

public class UI_ScorePopup : MonoBehaviour
{
    public Text easyScoreText;
    public Text normalScoreText;
    public Text hardScoreText;
    public Text hiddenScoreText;

    private void Start()
    {
        easyScoreText.text = $"쉬움: {PlayerPrefs.GetFloat("EasyScore")}";
        normalScoreText.text = $"보통: {PlayerPrefs.GetFloat("NormalScore")}";
        hardScoreText.text = $"어려움: {PlayerPrefs.GetFloat("HardScore")}";
        hiddenScoreText.text = $"???: {PlayerPrefs.GetFloat("HiddenScore")}";
    }

    public void OnClickBackButton()
    {
        gameObject.SetActive(false);
    }
}
