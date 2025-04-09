using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_SpecialModePopup : MonoBehaviour
{
    public Button ZombieButton;
    public Button InfiniteButton;

    public void OnClickZombieButton()
    {
        Managers.Instance.gameType = GameLevel.Zombie;
        Invoke("LoadMainScene", 1.0f);
    }

    public void OnClickInfiniteButton()
    {
        Managers.Instance.gameType = GameLevel.Infinite;
        Invoke("LoadMainScene", 1.0f);
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
