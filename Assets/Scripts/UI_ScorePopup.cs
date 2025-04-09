using UnityEngine;
using UnityEngine.UI;

public class UI_ScorePopup : MonoBehaviour
{
    public Text easyScoreText;
    public Text normalScoreText;
    public Text hardScoreText;
    public Text zombieModeScoreText;
    public Text infiniteModeScoreText;
    public Text hiddenScoreText;

    private void Start()
    {
        easyScoreText.text = $"����: {PlayerPrefs.GetFloat("EasyScore")}";
        normalScoreText.text = $"����: {PlayerPrefs.GetFloat("NormalScore")}";
        hardScoreText.text = $"�����: {PlayerPrefs.GetFloat("HardScore")}";
        zombieModeScoreText.text = $"���� ���: {PlayerPrefs.GetFloat("ZombieModeScore")}";
        infiniteModeScoreText.text = $"���� ���: {PlayerPrefs.GetInt("InfiniteModeScore")}";
        hiddenScoreText.text = $"???: {PlayerPrefs.GetFloat("HiddenScore")}";
    }

    public void OnClickBackButton()
    {
        gameObject.SetActive(false);
    }
}
