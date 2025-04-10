using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_InfiniteClearPopup : MonoBehaviour
{
    public Text maxRoundText;

    void Start()
    {
        maxRoundText.text = $"¸¶Áö¸· Round: {GameManager.Instance._Round}";
    }

    public void OnClickContinueButton()
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
