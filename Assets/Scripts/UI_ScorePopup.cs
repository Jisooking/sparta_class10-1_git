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
        easyScoreText.text = $"����: {PlayerPrefs.GetFloat("EasyScore"):N2}";
        normalScoreText.text = $"����: {PlayerPrefs.GetFloat("NormalScore"):N2}";
        hardScoreText.text = $"�����: {PlayerPrefs.GetFloat("HardScore"):N2}";
        zombieModeScoreText.text = $"���� ���: {PlayerPrefs.GetFloat("ZombieScore"):N2}";
        infiniteModeScoreText.text = $"���� ���: {PlayerPrefs.GetInt("InfiniteScore")}";
        hiddenScoreText.text = $"???: {PlayerPrefs.GetFloat("HiddenScore"):N2}";
    }

    public void OnClickBackButton()
    {
        gameObject.SetActive(false);
    }
}
