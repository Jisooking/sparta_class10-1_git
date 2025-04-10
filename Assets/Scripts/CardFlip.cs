using System.Collections;
using UnityEngine;

public class CardFlip : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clip;
    Animator anim;
    public GameObject front;
    public GameObject back;

    public Card card;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public void CardClick()
    {
        if (!GameManager.Instance.CanSelectCard())
        {
            return;
        }
        StartCoroutine(FlipCard());
    }

    IEnumerator FlipCard()
    {
        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = card;
        }
        else
        {
            GameManager.Instance.secondCard = card;

        }
        audioSource.PlayOneShot(clip);
        anim.SetBool("isOpen", true);

        //카드가 반쯤 뒤집힐 때까지 대기
        yield return new WaitForSeconds(0.5f);

        front.SetActive(true);
        back.SetActive(false);
        //카드가 완전히 뒤집힐 때까지 대기


        if (GameManager.Instance.firstCard != null && GameManager.Instance.secondCard != null)
        {
            yield return new WaitForSeconds(0.5f);
            GameManager.Instance.Matched();
        }
    }
}
