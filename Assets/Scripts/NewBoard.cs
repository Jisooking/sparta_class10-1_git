using UnityEngine;
using System.Linq;
using System.Collections;
public class NewBoard : MonoBehaviour
{
    public GameObject card;

    private GameObject[] cards = new GameObject[16]; //카드를 배열을 통해 관리

    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };

        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        for (int i = 0; i < 16; i++) //카드 생성 및 배열 할당
        {

            cards[i] = Instantiate(card, this.transform);
            //go.transform.position = new Vector2(x, y);
            cards[i].GetComponent<Card>().Setting(arr[i]);
        }
        for (int i = 0; i < 16; i++)   //카드 배치 
        {
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;
            StartCoroutine(MoveRoutine(cards[i].transform, new Vector2(x, y)));
        }

        GameManager.Instance.cardCount = arr.Length;

        //게임 시작 시 카드가 바로 펼쳐지지 않고, 약간 딜레이가 생겨서 오디오에도 약간 딜레이를 줌
        audioSource.PlayDelayed(1.0f);
    }

    IEnumerator MoveRoutine(Transform transform, Vector2 target)
    {
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
