using UnityEngine;
using System.Linq;
using System.Collections;
using Unity.VisualScripting;


public class Board : MonoBehaviour
{
    public GameObject card;

    private GameObject[] cards; //카드를 배열을 통해 관리

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //���� �ڵ�
        /*
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };

        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        for (int i = 0; i < 16; i++)
        {
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;
            GameObject go = Instantiate(card, this.transform);
            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
        }

        GameManager.Instance.cardCount = arr.Length;
        */

        //���̵��� ���� ���� ����
        InitBoard(Managers.Instance.gameType);
    }

    void InitBoard(GameLevel type)
    {
        switch (type)
        {
            case GameLevel.Easy:
                MakeEasyBoard();
                break;
            case GameLevel.Normal:
                MakeNormalBoard();
                break;
            case GameLevel.Hard:
                MakeHardBoard();
                break;
            case GameLevel.Hidden:
                MakeHiddenBoard();
                break;
        }
    }

    //���̵�-����: 6���� ī��
    void MakeEasyBoard()
    {

        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
        cards = new GameObject[arr.Length];

        arr = arr.OrderBy(x => Random.Range(0f, 5f)).ToArray();

        for (int i = 0; i < arr.Length; i++)
        {
            //float x = (i % 4) * 1.4f - 2.1f;
            //float y = (i / 4) * 1.4f - 3.0f;
            cards[i] = Instantiate(card, this.transform);
            //go.transform.position = new Vector2(x, y);
            cards[i].GetComponent<Card>().Setting(arr[i]);
        }

        for (int i = 0; i < arr.Length; i++)
        {
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;
            StartCoroutine(MoveRoutine(cards[i].transform, new Vector2(x, y)));
        }

        GameManager.Instance.cardCount = arr.Length;
    }

    //���̵�-���: 8���� ī��
    void MakeNormalBoard()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        cards = new GameObject[arr.Length];
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();


        for (int i = 0; i < arr.Length; i++)
        {
            //float x = (i % 4) * 1.4f - 2.1f;
            //float y = (i / 4) * 1.4f - 3.0f;
            cards[i] = Instantiate(card, this.transform);
            //go.transform.position = new Vector2(x, y);
            cards[i].GetComponent<Card>().Setting(arr[i]);
        }

        for (int i = 0; i < arr.Length; i++)
        {
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;
            StartCoroutine(MoveRoutine(cards[i].transform, new Vector2(x, y)));
        }
        GameManager.Instance.cardCount = arr.Length;
    }

    //���̵�-�ϵ�: 10���� ī��
    void MakeHardBoard()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };
        cards = new GameObject[arr.Length];
        arr = arr.OrderBy(x => Random.Range(0f, 14f)).ToArray();


        for (int i = 0; i < arr.Length; i++)
        {
            //float x = (i % 4) * 1.4f - 2.1f;
            //float y = (i / 4) * 1.4f - 3.0f;
            cards[i] = Instantiate(card, this.transform);
            //go.transform.position = new Vector2(x, y);
            cards[i].GetComponent<Card>().Setting(arr[i]);
        }

        for (int i = 0; i < arr.Length; i++)
        {
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.5f;
            StartCoroutine(MoveRoutine(cards[i].transform, new Vector2(x, y)));
        }
        GameManager.Instance.cardCount = arr.Length;

    }


    void MakeHiddenBoard()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };
        cards = new GameObject[arr.Length];
        arr = arr.OrderBy(x => Random.Range(0f, 14f)).ToArray();


        for (int i = 0; i < arr.Length; i++)
        {
            //float x = (i % 4) * 1.4f - 2.1f;
            //float y = (i / 4) * 1.4f - 3.0f;
            cards[i] = Instantiate(card, this.transform);
            //go.transform.position = new Vector2(x, y);
            cards[i].GetComponent<Card>().Setting(arr[i]);
        }

        for (int i = 0; i < arr.Length; i++)
        {
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.5f;
            StartCoroutine(MoveRoutine(cards[i].transform, new Vector2(x, y)));
        }
        GameManager.Instance.cardCount = arr.Length;

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
}
