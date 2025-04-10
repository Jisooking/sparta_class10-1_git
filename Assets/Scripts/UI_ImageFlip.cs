using UnityEngine;

public class UI_ImageFlip : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
     
    //UI 난이도/모드 선택 시 ImageFlip Animation 실행
    public void ImageFlip()
    {
        anim.SetTrigger("OnFlip");
    }
}
