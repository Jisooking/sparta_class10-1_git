using System.Collections;
using UnityEngine;

public class CardFlip : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clip;
    public Animator anim;
    public GameObject front;
    public GameObject back;

    public Card card;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void CardClick()
    {
        if (GameManager.Instance.cardOpening)
        {
            return;
        }
        if (GameManager.Instance.firstCard != null)
        {
            GameManager.Instance.cardOpening = true;
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
        //카드가 반쯤 뒤집힐때 까지 대기

        yield return new WaitForSeconds(0.5f);

        front.SetActive(true);
        back.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        if (GameManager.Instance.firstCard != null && GameManager.Instance.secondCard != null)
        {
            GameManager.Instance.Matched();
        }
    }
}
