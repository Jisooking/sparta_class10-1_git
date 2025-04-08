using System;
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
        }
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            time += 5.0f; //시간 추가
            AudioManager.Instance.PlayMatchSFX();
            firstCard.DestroyCard();
            secondCard.DestroyCard();

            cardCount -= 2;
            if (cardCount == 0)
            {
                GameClear();
            }
        }
        else
        {
            AudioManager.Instance.PlayFailSFX();
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;

        Invoke("SetBoolFalse", 0.5f); // 카드가 뒤집었을 때 마우스 클릭 딜레이
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

    void SetBoolFalse()
    {
        cardOpening = false;
    }
}

