using UnityEngine;
using UnityEngine.SceneManagement;

public class Hidden : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void GotoHiddenStage()
    {
        anim.SetTrigger("OnFlip");
        Invoke("GoToHiddenStageInvoke", 2.0f);

    }

    void GoToHiddenStageInvoke()
    {
        Debug.Log("히든 스테이지로 이동합니다!");
        Managers.Instance.gameType = GameLevel.Hidden;
        SceneManager.LoadScene("MainScene");
    }

}
