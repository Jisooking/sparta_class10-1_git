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
        LoadMainScene();
    }

    public void OnClickInfiniteButton()
    {
        Managers.Instance.gameType = GameLevel.Infinite;
        LoadMainScene();

    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
