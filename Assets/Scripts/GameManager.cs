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
    public float maxtime = 0f;

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

    private bool cardOpening;

    public bool isGameOver;

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
        firstCard = null;
        secondCard = null;
        cardOpening = false;
        Init();
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }
        time -= Time.deltaTime * timeScale;

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
                maxtime = 40.0f;
                break;
            case GameLevel.Normal:
                time = 60.0f;
                maxtime = 60.0f;
                break;
            case GameLevel.Hard:
                time = 90.0f;
                maxtime = 90.0f;
                break;
            case GameLevel.Hidden:
                time = 60.0f;
                maxtime = 60.0f;
                break;
            case GameLevel.Infinite:
                time = 60.0f;
                maxtime = 60.0f;
                break;
            case GameLevel.Zombie:
                zombieCount = 8;
                time = 60.0f;
                maxtime = 60.0f;
                break;
        }
    }

    public void Matched() //카드 매칭 확인
    {
        if (firstCard == null || secondCard == null)
        {
            return;
        }
        cardOpening = true;
        if (firstCard.idx == secondCard.idx)    //매칭 성공
        {
            if (Managers.Instance.gameType == GameLevel.Infinite) //무한 모드
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
                if (zombieCount == 0)
                {
                    GameOver();
                    return;
                }
                StartCoroutine(WaitAndActivate());  //카드 전부 활성화
            }
        }

        Invoke("AfterMatched", 0.5f); //0.5초 뒤, 카드 뒤집기 가능
    }

    public void GameStart() //게임 시작
    {
        isGameOver = false;
        AudioManager.Instance.PlayNormalBGM(); ;
    }

    public void GamePause() //게임 퍼즈
    {
        Time.timeScale = 0.0f;
        AudioManager.Instance.ControlBGM(false);
    }

    public void GameContinue()  //퍼즈 해제
    {
        Time.timeScale = 1.0f;
        AudioManager.Instance.ControlBGM(true);
    }

    public void GameStop()  //게임 멈춤
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

        if (PlayerPrefs.HasKey(typeKey))
        {
            if (typeKey == "ZombieScore")
            {
                float zombieCnt = (zombieCount*score < PlayerPrefs.GetFloat(typeKey) ? PlayerPrefs.GetFloat(typeKey) : zombieCount*score);
                PlayerPrefs.SetFloat(typeKey, zombieCnt);
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
        else
        {
            if (typeKey == "ZombieScore")
            {
                PlayerPrefs.SetFloat(typeKey, zombieCount*score);
            }
            else if (typeKey == "InfiniteScore")
            {
                PlayerPrefs.SetInt(typeKey, round);
            }
            else
            {
                PlayerPrefs.SetFloat(typeKey, score);
            }
        }
    }

    public bool CanSelectCard() //카드를 뒤집을 수 있는 상태인지 알려줌
    {
        return !cardOpening && secondCard == null;
    }

    void AfterMatched() //매칭 후, 다시 카드 뒤집기 가능한 상태로 돌아감
    {
        firstCard = null;
        secondCard = null;
        cardOpening = false;
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

