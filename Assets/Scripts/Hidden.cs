using UnityEngine;
using UnityEngine.SceneManagement;

public class Hidden : MonoBehaviour
{

    void GotoHiddenStage()
    {
        Debug.Log("히든 스테이지로 이동합니다!");
        Managers.Instance.gameType = GameLevel.Hidden;
        SceneManager.LoadScene("MainScene");
    }


}
