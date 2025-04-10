using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Card : MonoBehaviour
{
    public SpriteRenderer frontimage;
    public SpriteRenderer backImage;

    //난이도, 모드에 따라 지정할 뒷면 sprite
    public Sprite[] levelSprites;

    public GameObject front;
    public GameObject back;
    
    //카드 위치
    public int idx = 0;

    public Animator anim;
    public AudioClip clip;

    public AudioSource audioSource;

    //좀비 모드일 때 첫 4초 보여 줬는지 확인하기 위한 변수
    private bool hasShownZombieCard = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        //처음에 뒤집힌 채로 나타내기
        front.SetActive(false);
        back.SetActive(true);
        //좀비 모드일 경우 카드 자동 공개 루틴 실행
        if (Managers.Instance.gameType == GameLevel.Zombie && !hasShownZombieCard)
        {
            StartCoroutine(ShowAndHideZombieCard());
            hasShownZombieCard = true;
        }
    }

    void Update()
    {
        if (Managers.Instance.gameType != GameLevel.Hidden)
        {
            return;
        }

        //게임 모드 - 히든 모드일 때만 실행
        //x축과 y축이 외곽에 있을 경우 시계 방향으로 이동하도록 설정
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

        //x, y 좌표의 최댓값을 정해 주어서 시계 방향으로 이동할 때 조금씩 밀려나는 현상 방지
        if (transform.position.x <= -2.1f)
            transform.position = new Vector2(-2.1f, transform.position.y);
        else if (transform.position.x >= 2.1f)
            transform.position = new Vector2(2.1f, transform.position.y);
        else if (transform.position.y <= -3.5f)
            transform.position = new Vector2(transform.position.x, -3.5f);
        else if (transform.position.y >= 2.1f)
            transform.position = new Vector2(transform.position.x, 2.1f);
    }

    //카드 본인의 인덱스, 앞면과 뒷면 사진 설정
    public void Setting(int num)
    {
        idx = num;
        frontimage.sprite = Resources.Load<Sprite>($"1jo{idx}");
        SetLevelImage(Managers.Instance.gameType);
    }

    //카드 뒷면으로 뒤집기
    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    //카드 제거
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

    //카드 뒷면으로 뒤집기
    public void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }

    //카드 비활성화(좀비 모드, 무한 모드) > 재활용을 위해 뒷면이 나올 수 있도록 설정
    public void DisableCardInvoke()
    {
        gameObject.SetActive(false);
        front.SetActive(false);
        back.SetActive(true);
    }

    //카드 뒷면 이미지 설정
    public void SetLevelImage(GameLevel level)
    {
        backImage.sprite = levelSprites[(int)level];
    }

    //좀비 모드의 경우 첫 시작에 4초 동안 모든 카드 보여 주기
    IEnumerator ShowAndHideZombieCard()
    {
        yield return new WaitForSeconds(2f);
        //앞면 보여 주기
        front.SetActive(true);
        back.SetActive(false);

        //4초 대기
        yield return new WaitForSeconds(4f); 

        //다시 뒷면으로 뒤집기
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
}
