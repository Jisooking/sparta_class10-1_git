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
    //난이도 해금
    public bool isEasyCleared;
    public bool isNormalCleared;
    public bool isHardCleard;

    //난이도 점수 저장
    public float easyScore;
    public float normalScore;
    public float hardScore;

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
        float score = time;
        string typeKey = "";
        //난이도에 따른 해금 조건, 점수 저장
        switch(gameType)
        {
            case GameLevel.Easy:
                if (!isEasyCleared)
                {
                    isEasyCleared = true;
                }
                //점수
                typeKey = "EasyScore";
                break;
            case GameLevel.Normal:
                if (!isNormalCleared)
                {
                    isNormalCleared = true;
                }
                typeKey = "NormalScore";
                break;
            case GameLevel.Hard:
                if (!isHardCleard)
                {
                    isHardCleard = true;
                }
                typeKey = "HardScore";
                break;
        }

        //점수 저장
        if (PlayerPrefs.HasKey(typeKey))
            score = (score > PlayerPrefs.GetFloat(typeKey) ? PlayerPrefs.GetFloat(typeKey) : score);
        PlayerPrefs.SetFloat(typeKey, score);

        Debug.Log($"{typeKey}: {PlayerPrefs.GetFloat(typeKey)}");

        endTxt.SetActive(true);
        Time.timeScale = 0.0f;
    }
}

