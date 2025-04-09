using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Hidden : MonoBehaviour
{
    void GotoHiddenStage()
    {

        Debug.Log("히든 스테이지로 이동합니다!");
        Managers.Instance.gameType = GameLevel.Hidden;
        LoadMainScene();
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

}
