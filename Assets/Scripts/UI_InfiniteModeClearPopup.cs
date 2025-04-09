using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_InfiniteModeClearPopup : MonoBehaviour
{
    public Text roundText;

    private void Start()
    {
        roundText.text = $"최종 라운드: {GameManager.Instance._Round}";
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
