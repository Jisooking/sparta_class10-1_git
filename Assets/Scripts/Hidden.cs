using UnityEngine;
using UnityEngine.SceneManagement;

public class Hidden : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    //Flipper.cs에서 Rtan Image와 Flipper Image가 겹쳐졌을 때 호출되는 함수
    void GotoHiddenStage()
    {
        //RtanFlip Animation 실행
        anim.SetTrigger("OnFlip");
        //애니메이션 실행 후 히든 스테이지로 이동
        Invoke("GoToHiddenStageInvoke", 2.0f);
    }

    //히든 모드 설정 후 MainScene으로 이동
    void GoToHiddenStageInvoke()
    {
        Debug.Log("히든 스테이지로 이동합니다!");
        Managers.Instance.gameType = GameLevel.Hidden;
        SceneManager.LoadScene("MainScene");
    }

}
