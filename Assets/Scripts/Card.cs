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
        
        if (GameManager.Instance.gameType != GameLevel.Hidden)
        {
            return;
        }
        if (gameObject.transform.position.x <= -2.1f)
        {
            if (gameObject.transform.position.y >= 2.1f)
            {
                gameObject.transform.position += new Vector3(0.05f, 0f, 0f);
            }
            gameObject.transform.position += new Vector3(0f, 0.05f, 0f);
        }
        else if (gameObject.transform.position.y <= -3.5f)
        {
            gameObject.transform.position -= new Vector3(0.05f, 0f, 0f);
        }
        else if (gameObject.transform.position.x >= 2.1f)
        {
            gameObject.transform.position -= new Vector3(0f, 0.05f, 0f);
        }
        else if (gameObject.transform.position.y >= 2.1f)
        {
            gameObject.transform.position += new Vector3(0.05f, 0f, 0f);
        }
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
