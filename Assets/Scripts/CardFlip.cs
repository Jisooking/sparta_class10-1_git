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
        card = GetComponent<Card>();
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
        if (GameManager.Instance.firstCard == card) //같은 카드 두번 클릭 방지
        {
            yield break;
        }
        if (GameManager.Instance.firstCard == null) //첫번째 카드 할당
        {
            GameManager.Instance.firstCard = card;
        }
        else
        {
            GameManager.Instance.secondCard = card; //두번째 카드 할당
        }

        audioSource.PlayOneShot(clip);
        anim.SetBool("isOpen", true);

        //카드가 반쯤 뒤집힐 때까지 대기
        yield return new WaitForSeconds(0.5f);

        front.SetActive(true);
        back.SetActive(false);
        //카드가 완전히 뒤집힐 때까지 대기

        if (GameManager.Instance.secondCard == card) //두번째 카드인 경우만 매칭 시도
        {
            yield return new WaitForSeconds(0.5f);
            GameManager.Instance.Matched();
        }
    }
}
