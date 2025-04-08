using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryBtn : MonoBehaviour
{
    public GameObject GameLevelTitle;
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void LoadLevel()
    {
        GameLevelTitle.SetActive(true);
    }
}
