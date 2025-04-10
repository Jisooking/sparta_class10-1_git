using UnityEngine;
using System.Linq;
using System.Collections;
using Unity.VisualScripting;


public class Board : MonoBehaviour
{
    public GameObject card;

    //카드를 배열을 통해 관리
    private GameObject[] cards; 

    //카드 위치를 지정하기 위한 x, y distance 상수값
    private float xDistance = 2.1f;
    private float yDistance = 3.0f;

    void Start()
    {   
        //일반 모드에서는 바로 카드 배치
        if (Managers.Instance.gameType == GameLevel.Easy ||
          Managers.Instance.gameType == GameLevel.Normal ||
          Managers.Instance.gameType == GameLevel.Hard)
        {
            InitBoard();
        }
    }

    //초기 보드 세팅
    public void InitBoard()
    {
        switch (Managers.Instance.gameType)
        {
            case GameLevel.Easy:
                MakeBoard(12);
                break;
            case GameLevel.Normal:
                MakeBoard(16);
                break;
            case GameLevel.Hard:
                yDistance += 0.5f;
                MakeBoard(20);
                break;
            case GameLevel.Hidden:
                yDistance += 0.5f;
                MakeBoard(20);
                break;
            case GameLevel.Infinite:
                MakeBoard(4);
                break;
            case GameLevel.Zombie:
                MakeBoard(16);
                break;
        }
    }

    //보드 생성(매개변수: 카드 개수)
    void MakeBoard(int boardSize)
    {
        //arr = { 0, 0, 1, 1, 2, 2, ..., boardSize/2, boardSize/2 };
        int[] arr = new int[boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            arr[i] = i / 2;
        }

        arr = arr.OrderBy(x => Random.Range(0f, 5f)).ToArray();

        //카드가 처음 생성될 때 중앙에서 원래 자리로 퍼져나가는 모습을 연출하기 위해 cards 배열에 저장
        cards = new GameObject[boardSize];

        for (int i = 0; i < arr.Length; i++)
        {
            cards[i] = Instantiate(card, this.transform);
            //카드 본인의 인덱스, 앞면과 뒷면 사진 세팅
            cards[i].GetComponent<Card>().Setting(arr[i]);
        }

        //배치되어야 할 위치 지정
        for (int i = 0; i < arr.Length; i++)
        {
            float x = (i % 4) * 1.4f - xDistance;
            float y = (i / 4) * 1.4f - yDistance;
            //처음에 중앙에 배치되었다가 본인 자리로 가는 함수 코루틴 실행
            StartCoroutine(MoveRoutine(cards[i].transform, new Vector2(x, y)));
        }

        //총 맞춰야 할 카드 개수 저장
        GameManager.Instance.cardCount = arr.Length;
        //카드 배치 이후 게임 시작
        Invoke("StartGame", 1.9f);
    }

    //카드 재배치, 무한 모드에서 사용
    public void ShuffleCards() 
    {
        //배열의 순서를 무작위로 변경
        for (int i = 0; i < cards.Length; i++) 
        {
            int rnd = Random.Range(i, cards.Length);
            GameObject temp = cards[i];
            cards[i] = cards[rnd];
            cards[rnd] = temp;
        }

        //카드 펼치기
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].gameObject.SetActive(true);
            cards[i].transform.position = transform.position;
            float x = (i % 4) * 1.4f - xDistance;
            float y = (i / 4) * 1.4f - yDistance;
            StartCoroutine(MoveRoutine(cards[i].transform, new Vector2(x, y)));
        }
        GameManager.Instance.cardCount = cards.Length;
    }

    //카드 전부 활성화, 좀비 모드에서 사용
    public void ActivateCards() 
    {
        GameManager.Instance.cardCount = cards.Length;
        foreach (GameObject card in cards)
        {
            card.SetActive(true);
        }
    }

    //중앙에 있던 카드들이 본인 위치로 이동하는 코루틴 함수
    IEnumerator MoveRoutine(Transform transform, Vector2 target)
    {
        AudioManager.Instance.PlayShuffleSound();
        Vector2 start = transform.position;
        //카드를 세로로 먼저 펼치기 위한 좌표
        Vector2 line = new Vector2(start.x, target.y);
        //Lerp 메서드를 위한 보간 계수
        float elapsed = 0f;

        //카드를 세로로 펼치기
        while (elapsed < 1.0f)  
        {

            transform.position = Vector3.Lerp(start, line, elapsed / 1.0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        //카드를 가로로 펼치기
        while (elapsed < 1.0f) 
        {
            transform.position = Vector3.Lerp(line, target, elapsed / 1.0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        //최종 위치 보정
        transform.position = target; 

    }

    //게임 시작을 알리는 메서드
    void StartGame() 
    {
        GameManager.Instance.GameStart();
    }

}
