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

    public Card firstCard;
    public Card secondCard;

    public Board board;
    float time;
    public float _Time
    {
        get { return time; }
        set { time = value; }
    }

    public int cardCount;

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

    void Start()
    {
        Time.timeScale = 1.0f;
        Application.targetFrameRate = 60;

        SetTime();
    }

    void Update()
    {
        if (isGameOver)
        {
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
            GameManager.Instance.GameOver();
        }


    }

    public void SetTime()
    {
        switch (Managers.Instance.gameType)
        {
            case GameLevel.Easy:
                time = 60.0f;
                break;
            case GameLevel.Normal:
                time = 30.0f;
                break;
            case GameLevel.Hard:
                time = 30.0f;
                break;
            case GameLevel.Hidden:
                time = 5.0f;
                break;
            case GameLevel.Infinite:
                time = 60.0f;
                break;
            case GameLevel.Zombie:
                time = 60.0f;
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
            time += 5.0f; //시간 추가
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
                StartCoroutine(WaitAndActivate());  //카드 전부 활성화
                time -= 5.0f; //시간 감소
            }

        }

        firstCard = null;
        secondCard = null;
        cardOpening = false;
    }

    public void GameOver()
    {

        Time.timeScale = 0.0f;
        isGameOver = true;
        GameOverEvent.Invoke();

        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayGameOverSFX();
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
        }

        //
        if (PlayerPrefs.HasKey(typeKey))
            score = (score < PlayerPrefs.GetFloat(typeKey) ? PlayerPrefs.GetFloat(typeKey) : score);
        PlayerPrefs.SetFloat(typeKey, score);

    }

    public void GameClear()
    {
        GameClearEvent.Invoke();
        AudioManager.Instance.StopBGM();
        Time.timeScale = 0.0f;
    }

    public bool CanSelectCard()
    {
        return !cardOpening && secondCard == null;
    }

    IEnumerator WaitAndShuffle()
    {
        yield return new WaitForSeconds(0.6f); // 카드 비활성화까지 대기
        board.ShuffleCards();
    }
    IEnumerator WaitAndActivate()
    {
        yield return new WaitForSeconds(0.6f); // 카드 비활성화까지 대기
        board.ActivateCards();
    }
}

