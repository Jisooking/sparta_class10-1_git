using UnityEngine;
using System.Linq;
using System.Collections;
using Unity.VisualScripting;


public class Board : MonoBehaviour
{
    public GameObject card;

    private GameObject[] cards; //카드를 배열을 통해 관리


    private float xDistance = 2.1f;
    private float yDistance = 3.0f;


    void Start()
    {   //일반 모드에서는 바로 카드 배치
        if (Managers.Instance.gameType == GameLevel.Easy ||
          Managers.Instance.gameType == GameLevel.Normal ||
          Managers.Instance.gameType == GameLevel.Hard)
        {
            InitBoard();
        }
    }
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

    void MakeBoard(int boardSize)
    {
        int[] arr = new int[boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            arr[i] = i / 2;
        }

        arr = arr.OrderBy(x => Random.Range(0f, 5f)).ToArray();

        cards = new GameObject[boardSize];

        for (int i = 0; i < arr.Length; i++)
        {
            cards[i] = Instantiate(card, this.transform);
            cards[i].GetComponent<Card>().Setting(arr[i]);
        }

        for (int i = 0; i < arr.Length; i++)
        {
            float x = (i % 4) * 1.4f - xDistance;
            float y = (i / 4) * 1.4f - yDistance;
            StartCoroutine(MoveRoutine(cards[i].transform, new Vector2(x, y)));
        }

        GameManager.Instance.cardCount = arr.Length;
        Invoke("StartGame", 1.9f);
    }


    public void ShuffleCards() //카드 재배치, 무한 모드에서 사용
    {
        for (int i = 0; i < cards.Length; i++) //배열의 순서를 무작위로 변경
        {
            int rnd = Random.Range(i, cards.Length);
            GameObject temp = cards[i];
            cards[i] = cards[rnd];
            cards[rnd] = temp;
        }

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

    public void ActivateCards() //카드 전부 활성화, 좀비 모드에서 사용
    {
        GameManager.Instance.cardCount = cards.Length;
        foreach (GameObject card in cards)
        {
            card.SetActive(true);
        }
    }


    IEnumerator MoveRoutine(Transform transform, Vector2 target)
    {
        AudioManager.Instance.PlayShuffleSound();
        Vector2 start = transform.position;
        Vector2 line = new Vector2(start.x, target.y); //카드를 세로로 먼저 펼치기 위한 좌표
        float elapsed = 0f; // Lerp 메서드를 위한 보간 계수

        while (elapsed < 1.0f)  //카드를 세로로 펼치기
        {

            transform.position = Vector3.Lerp(start, line, elapsed / 1.0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        while (elapsed < 1.0f)  //카드를 가로로 펼치기
        {
            transform.position = Vector3.Lerp(line, target, elapsed / 1.0f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.position = target; // 최종 위치 보정

    }

    void StartGame() //게임 시작을 알리는 메서드
    {
        GameManager.Instance.GameStart();
    }

}
