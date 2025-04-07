using UnityEngine;
using System.Linq;
using System.Collections;
public class NewBoard : MonoBehaviour
{
    public GameObject card;

    private GameObject[] cards = new GameObject[16];

    private AudioSource audioSource;
    public AudioClip shuffleClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };

        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        for (int i = 0; i < 16; i++)
        {

            cards[i] = Instantiate(card, this.transform);
            //go.transform.position = new Vector2(x, y);
            cards[i].GetComponent<Card>().Setting(arr[i]);
        }
        for (int i = 0; i < 16; i++)
        {
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;
            Debug.Log(i);
            StartCoroutine(MoveRoutine(cards[i].transform, new Vector2(x, y)));
        }

        GameManager.Instance.cardCount = arr.Length;

        audioSource.PlayDelayed(1.0f);
    }

    IEnumerator MoveRoutine(Transform transform, Vector2 target)
    {
        Vector2 start = transform.position;
        Vector2 row = new Vector2(start.x, target.y);
        float elapsed = 0f;

        while (elapsed < 1.0f)  //카드를 세로로 펼치기
        {

            transform.position = Vector3.Lerp(start, row, elapsed / 1.0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        while (elapsed < 1.0f)  //카드를 가로로 펼치기
        {
            transform.position = Vector3.Lerp(row, target, elapsed / 1.0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = target; // 최종 위치 보정
    }
}
