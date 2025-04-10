using UnityEngine;

public class UI_ImageFlip : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void ImageFlip()
    {
        anim.SetTrigger("OnFlip");
        AudioManager.Instance.PlayImageFLipSFX();
    }
}
