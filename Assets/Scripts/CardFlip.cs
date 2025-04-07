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
        StartCoroutine(FlipCard());
    }

    IEnumerator FlipCard()
    {
        audioSource.PlayOneShot(clip);
        anim.SetBool("isOpen", true);

        yield return new WaitForSeconds(0.5f);

        front.SetActive(true);
        back.SetActive(false);
        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = card;
        }
        else
        {
            GameManager.Instance.secondCard = card;
            GameManager.Instance.Matched();
        }

    }
}
