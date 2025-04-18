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
        easyScoreText.text = $"점수: {PlayerPrefs.GetFloat("EasyScore"):N2}";
        normalScoreText.text = $"보통: {PlayerPrefs.GetFloat("NormalScore"):N2}";
        hardScoreText.text = $"어려움: {PlayerPrefs.GetFloat("HardScore"):N2}";
        zombieModeScoreText.text = $"좀비 모드: {PlayerPrefs.GetFloat("ZombieScore"):N2}";
        infiniteModeScoreText.text = $"무한 모드: {PlayerPrefs.GetInt("InfiniteScore")}";
        hiddenScoreText.text = $"???: {PlayerPrefs.GetFloat("HiddenScore"):N2}";
    }

    public void OnClickBackButton()
    {
        gameObject.SetActive(false);
    }
}
