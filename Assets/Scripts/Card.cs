using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Card : MonoBehaviour
{
    public SpriteRenderer frontimage;
    public SpriteRenderer backImage;
    public Sprite[] levelSprites;
    public GameObject front;
    public GameObject back;
    public int idx = 0;
    public Animator anim;
    public AudioClip clip;
    public AudioSource audioSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        front.SetActive(false);
        back.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Managers.Instance.gameType != GameLevel.Hidden)
        {
            return;
        }

        //�ܰ��� �ִ� ī��� �ð� �������� �̵�
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

        //�ʰ��Ǵ� �κ� x, y �����ϱ�
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
        SetLevelImage(Managers.Instance.gameType);
    }
    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    public void DestroyCard()
    {
        //무한 모드나 좀비모드의 경우, 카드 재활용 위해 비활성화
        if (Managers.Instance.gameType == GameLevel.Infinite
           || Managers.Instance.gameType == GameLevel.Zombie)
        {
            Invoke("DisableCardInvoke", 0.5f);

        }
        else
        {
            Destroy(gameObject, 0.5f);
        }
    }

    public void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
    public void DisableCardInvoke()
    {
        gameObject.SetActive(false);
        front.SetActive(false);
        back.SetActive(true);
    }

    public void SetLevelImage(GameLevel level)
    {
        if ((int)level < levelSprites.Length)
        {
            backImage.sprite = levelSprites[(int)level];
        }
    }
}
