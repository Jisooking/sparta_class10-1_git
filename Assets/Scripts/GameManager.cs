using UnityEngine;
using UnityEngine.UI;

public enum GameLevel
{
    Easy,
    Normal,
    Hard,
    Hidden,
}

public class GameManager : MonoBehaviour
{
    public bool isEasyCleared;
    public bool isNormalCleared;
    public bool isHardCleard;

    public static GameManager Instance;
    public Card firstCard;
    public Card secondCard;
    public Text timeTxt;
    float time = 0.0f;
    public int cardCount;
    public GameObject endTxt;
    public AudioSource audioSource;
    public AudioClip clip;

    public GameLevel gameType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if(Instance == null)
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
        if(time > 30.0f)
        {
            time = 30.0f;
            GameOver();
        }
    }
    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
        {   
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();

            cardCount -= 2;
            if(cardCount == 0)
            {
                GameOver();
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }
    public void GameOver()
    {
        //스테이지 해금 조건(추후에 횟수 같은 거 추가해서 넣을 수 있음)
        switch(gameType)
        {
            case GameLevel.Easy:
                if (!isEasyCleared)
                {
                    isNormalCleared = true;
                }
                break;
            case GameLevel.Normal:
                if (!isNormalCleared)
                {
                    isHardCleard = true;
                }
                break;
            case GameLevel.Hard:
                break;
        }

        endTxt.SetActive(true);
        Time.timeScale = 0.0f;
    }
}

