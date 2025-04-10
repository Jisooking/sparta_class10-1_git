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
        gameObject.tag = "Untagged";
        AudioManager.Instance.PlayImageFLipSFX();
        Invoke("GoToHiddenStageInvoke", 2.0f);

    }

    void GoToHiddenStageInvoke()
    {
        Managers.Instance.gameType = GameLevel.Hidden;

        SceneManager.LoadScene("MainScene");
    }

}
