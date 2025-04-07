using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Card firstCard;
    public Card secondCard;
    public Text timeTxt;
    float time = 0.0f;
    public int cardCount;
    public GameObject endTxt;
    public AudioSource audioSource;
    public AudioClip clip;

    public AudioClip failClip;
    public AudioClip hurryUpClip;
    public AudioClip gameOverClip;

    public AudioSource bgm;

    bool isMusicChanged = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        if (time > 20.0f && !isMusicChanged) //20초 지나면 긴박한 브금 재생
        {
            bgm.Stop();
            bgm.clip = hurryUpClip;
            bgm.Play();
            isMusicChanged = true;
        }
        else if (time > 30.0f)
        {
            time = 30.0f;
            bgm.Stop();
            audioSource.PlayOneShot(gameOverClip);
            GameOver();
        }
    }
    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();

            cardCount -= 2;
            if (cardCount == 0)
            {
                GameOver();
            }
        }
        else
        {
            audioSource.PlayOneShot(failClip);
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }
    public void GameOver()
    {
        endTxt.SetActive(true);
        Time.timeScale = 0.0f;
    }
}

