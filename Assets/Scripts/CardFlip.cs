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
        anim = GetComponent<Animator>();
        card = GetComponent<Card>();
    }

    //카드 클릭 시 수행되는 함수
    public void CardClick()
    {
        //카드를 선택할 수 없다면 return > 카드를 선택한 후 해당 애니메이션이 전부 진행되고 나서야 다음 카드를 선택할 수 있도록
        if (!GameManager.Instance.CanSelectCard())
        {
            return;
        }
        //카드 뒤집기 코루틴 함수 실행
        StartCoroutine(FlipCard());
    }


    IEnumerator FlipCard()
    {
        if (GameManager.Instance.firstCard == card) //같은 카드 두번 클릭 방지
        {
            yield break;
        }

        //첫 번째 카드인지, 두 번째 카드인지 확인 후 GameManager에 알리기
        if (GameManager.Instance.firstCard == null) 
        {
            GameManager.Instance.firstCard = card;
        }
        else
        {
            GameManager.Instance.secondCard = card;
        }

        //카드 선택 오디오 실행
        audioSource.PlayOneShot(clip);
        //카드 뒤집기 애니메이션 실행
        anim.SetBool("isOpen", true);

        //카드가 반쯤 뒤집힐 때까지 대기
        yield return new WaitForSeconds(0.5f);

        //카드 앞면 보여 주기
        front.SetActive(true);
        back.SetActive(false);

        //두 번째 카드인 경우만 매칭 시도
        if (GameManager.Instance.secondCard == card) 
        {
            yield return new WaitForSeconds(0.5f);
            GameManager.Instance.Matched();
        }
    }
}
