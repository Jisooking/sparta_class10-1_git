using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Card : MonoBehaviour
{
    public SpriteRenderer frontimage;
    public GameObject front;
    public GameObject back;
    public int idx = 0;
    public Animator anim;
    public AudioClip clip;
    public AudioSource audioSource;
    public bool setOpen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Managers.Instance.gameType != GameLevel.Hidden)
        {
            return;
        }
        
        //외곽에 있는 카드들 시계 방향으로 이동
        if (transform.position.x <= -2.1f)
        {
            if (transform.position.y >= 2.1f)
            {
                transform.position += new Vector3(0.05f, 0f, 0f);
            }
            transform.position += new Vector3(0f, 0.05f, 0f);
        }
        else if (transform.position.y <= -3.5f)
        {
            transform.position -= new Vector3(0.05f, 0f, 0f);
        }
        else if (transform.position.x >= 2.1f)
        {
            transform.position -= new Vector3(0f, 0.05f, 0f);
        }
        else if (transform.position.y >= 2.1f)
        {
            transform.position += new Vector3(0.05f, 0f, 0f);
        }

        //초과되는 부분 x, y 고정하기
        if (transform.position.x <= -2.1f)
            transform.position = new Vector2(-2.1f, transform.position.y);
        else if (transform.position.x >= 2.1f)
            transform.position = new Vector2(2.1f, transform.position.y);
        else if (transform.position.y <= -3.5f)
            transform.position = new Vector2(transform.position.x, -3.5f);
        else if (transform.position.y >= 2.1f)
            transform.position = new Vector2(transform.position.x, 2.1f);
    }

    public void Setting(int num)
    {
        idx = num;
        frontimage.sprite = Resources.Load<Sprite>($"1jo{idx}");
    }

    public void OpenCard()
    {
        {
            audioSource.PlayOneShot(clip);
            anim.SetBool("isOpen", true);


            front.SetActive(true);
            back.SetActive(false);
            if (GameManager.Instance.firstCard == null)
            {
                GameManager.Instance.firstCard = this;
            }
            else
            {
                //GameManager.Instance.cardOpening = true;
                GameManager.Instance.secondCard = this;
                GameManager.Instance.Matched();
            }
        }
    }
    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    public void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
    public void DestroyCard()
    {
        Destroy(gameObject, 0.5f);
    }


    void OpenImage()
    {
        if (!setOpen)
        {
            front.SetActive(false);
            back.SetActive(true);
        }
        else
        {
            front.SetActive(true);
            back.SetActive(false);
        }
    }
}
