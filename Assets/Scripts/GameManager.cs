using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event Action GameOverEvent;
    public event Action GameClearEvent;
    public event Action CardMatchEvent;

    public Card firstCard;
    public Card secondCard;

    public Board board;
    float time;
    int round;
    public float _Time
    {
        get { return time; }
        set { time = value; }
    }

    public int _Round
    {
        get { return round; }
        set { round = value; }
    }

    public int cardCount;

    private float timeScale = 1.0f; //시간 가속 - 무한 모드에서 사용

    public bool cardOpening = false;

    public bool isGameOver { get; private set; }

    bool isMusicChanged = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public event Action ZombieCountChanged;
    public int zombieCount;

    void Start()
    {
        Time.timeScale = 1.0f;
        timeScale = 1.0f;
        Application.targetFrameRate = 60;
        isGameOver = true;
        round = 1;
        Init();
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }
        time -= Time.deltaTime * timeScale;
        if (Managers.Instance.gameType == GameLevel.Zombie)
        {
            if (zombieCount == 0)
            {
                GameOver();
            }
            return;
        }

        time -= Time.deltaTime;

        if (time <= 10.0f && !isMusicChanged)
        {
            AudioManager.Instance.StopBGM();
            AudioManager.Instance.PlayHurryUpBGM();
            isMusicChanged = true;

        }

        if (time <= 0.0f)
        {
            time = 0.0f;
            GameOver();
        }
    }

    public void Init()
    {
        switch (Managers.Instance.gameType)
        {
            case GameLevel.Easy:
                time = 40.0f;
                break;
            case GameLevel.Normal:
                time = 60.0f;
                break;
            case GameLevel.Hard:
                time = 90.0f;
                break;
            case GameLevel.Hidden:
                time = 60.0f;
                break;
            case GameLevel.Infinite:
                time = 60.0f;
                break;
            case GameLevel.Zombie:
                zombieCount = 8;
                break;
        }
    }

    public void Matched()
    {
        if (firstCard == null || secondCard == null)
        {
            return;
        }

        cardOpening = true;
        if (firstCard.idx == secondCard.idx)    //매칭 성공
        {
            if (Managers.Instance.gameType == GameLevel.Infinite)
            {
                time += 5.0f; //시간 추가
                CardMatchEvent.Invoke();
            }
            AudioManager.Instance.PlayMatchSFX();
            firstCard.DestroyCard();
            secondCard.DestroyCard();

            cardCount -= 2;
            if (cardCount == 0) //카드 매칭 전부 성공
            {
                if (Managers.Instance.gameType == GameLevel.Infinite) //무한 모드인 경우
                {
                    StartCoroutine(WaitAndShuffle()); //카드 재배치

                }
                else
                {
                    GameClear();
                }
            }
        }
        else    //매칭 실패
        {
            AudioManager.Instance.PlayFailSFX();
            firstCard.CloseCard();
            secondCard.CloseCard();
            if (Managers.Instance.gameType == GameLevel.Zombie) //좀비 모드인 경우
            {
                zombieCount--;
                ZombieCountChanged.Invoke();
                StartCoroutine(WaitAndActivate());  //카드 전부 활성화
            }
        }

        firstCard = null;
        secondCard = null;
        cardOpening = false;
    }

    public void GameStart()
    {
        isGameOver = false;
    }

    public void GameStop()
    {
        isGameOver = true;
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        isGameOver = true;
        GameOverEvent.Invoke();

        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayGameOverSFX();

        if (Managers.Instance.gameType == GameLevel.Infinite)
        {
            string typeKey = "InfiniteScore"; ;
            int roundCnt = (round < PlayerPrefs.GetInt(typeKey) ? PlayerPrefs.GetInt(typeKey) : round);
            PlayerPrefs.SetInt(typeKey, roundCnt);
        }
    }

    public void GameClear()
    {
        GameClearEvent.Invoke();
        AudioManager.Instance.StopBGM();
        Time.timeScale = 0.0f;

        float score = time;
        string typeKey = "";
        //
        switch (Managers.Instance.gameType)
        {
            case GameLevel.Easy:
                Managers.Instance.unlockNormal = true;
                typeKey = "EasyScore";
                break;
            case GameLevel.Normal:
                Managers.Instance.unlockHard = true;
                typeKey = "NormalScore";
                break;
            case GameLevel.Hard:
                typeKey = "HardScore";
                break;
            case GameLevel.Hidden:
                typeKey = "HiddenScore";
                break;
            case GameLevel.Infinite:
                typeKey = "InfiniteScore";
                break;
            case GameLevel.Zombie:
                typeKey = "ZombieScore";
                break;
        }

        //
        if (PlayerPrefs.HasKey(typeKey))
        {
            if (typeKey == "ZombieScore")
            {
                int zombieCnt = (zombieCount < PlayerPrefs.GetInt(typeKey) ? PlayerPrefs.GetInt(typeKey) : zombieCount);
                PlayerPrefs.SetInt(typeKey, zombieCnt);
            }
            else if (typeKey == "InfiniteScore")
            {
                int roundCnt = (round < PlayerPrefs.GetInt(typeKey) ? PlayerPrefs.GetInt(typeKey) : round);
                PlayerPrefs.SetInt(typeKey, roundCnt);
            }
            else
            {
                score = (score < PlayerPrefs.GetFloat(typeKey) ? PlayerPrefs.GetFloat(typeKey) : score);
                PlayerPrefs.SetFloat(typeKey, score);
            }
        }
    }

    public bool CanSelectCard()
    {
        return !cardOpening && secondCard == null;
    }

    IEnumerator WaitAndShuffle()
    {
        yield return new WaitForSeconds(0.6f); // 카드 비활성화까지 대기
        board.ShuffleCards();
        round += 1; // 라운드 + 1
        timeScale *= 1.2f;  //시간 점점 빠르게
    }
    IEnumerator WaitAndActivate()
    {
        yield return new WaitForSeconds(0.6f); // 카드 비활성화까지 대기
        board.ActivateCards();
    }
}

