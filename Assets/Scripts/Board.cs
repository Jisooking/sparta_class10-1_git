using UnityEngine;
using System.Linq;


public class Board : MonoBehaviour
{
    public GameObject card;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //원래 코드
        /*
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };

        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        for(int i = 0; i <16; i++)
        {
            float x = (i % 4) * 1.4f -2.1f;
            float y = (i / 4) * 1.4f - 3.0f;
            GameObject go = Instantiate(card, this.transform);
            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
        }

        GameManager.Instance.cardCount = arr.Length;
        */

        //난이도에 따른 보드 생성
        InitBoard(GameManager.Instance.gameType);
    }

    void InitBoard(GameLevel type)
    {
        switch(type)
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
        }
    }

    void MakeEasyBoard()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };

        arr = arr.OrderBy(x => Random.Range(0f, 5f)).ToArray();

        for (int i = 0; i < arr.Length; i++)
        {
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;
            GameObject go = Instantiate(card, this.transform);
            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
        }

        GameManager.Instance.cardCount = arr.Length;
    }

    void MakeNormalBoard()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };

        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        for (int i = 0; i < arr.Length; i++)
        {
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;
            GameObject go = Instantiate(card, this.transform);
            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
        }

        GameManager.Instance.cardCount = arr.Length;
    }

    void MakeHardBoard()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14 };

        arr = arr.OrderBy(x => Random.Range(0f, 14f)).ToArray();

        for (int i = 0; i < arr.Length; i++)
        {
            float x = (i % 5) * 1.4f - 2.1f;
            float y = (i / 5) * 1.4f - 3.0f;
            GameObject go = Instantiate(card, this.transform);
            go.transform.position = new Vector2(x, y);
            //사이즈 조절이 안 됨 >> 왜? animation 때문에
            //
            go.GetComponent<Card>().Setting(arr[i]);
        }

        GameManager.Instance.cardCount = arr.Length;
    }
}
