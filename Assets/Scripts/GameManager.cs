using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float maxtime = 0f;
    float time;
    public float _Time
    {
        get { return time; }
        set { time = value; }
    }

    int round;
    public int _Round
    {
        get { return round; }
        set { round = value; }

    }

    public Card firstCard;
    public Card secondCard;

    public Board board;

    //총 카드 개수
    public int cardCount;

    //무한 모드 - 시간 가속을 위해 사용
    private float timeScale = 1.0f; 

    //카드 오픈 중인지 확인하는 변수
    private bool cardOpening;

    //게임 진행 중인지 확인하는 변수
    public bool isGameOver;

    //음악이 변경됐는지 확인하는 변수
    bool isMusicChanged = false;

    //좀비 모드 - 뒤집기 횟수를 확인하는 변수
    public int zombieCount;

    //이벤트 
    public event Action ZombieCountChanged;
    public event Action GameOverEvent;
    public event Action GameClearEvent;
    public event Action CardMatchEvent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Application.targetFrameRate = 60;

        Time.timeScale = 1.0f;

        isGameOver = true;

        timeScale = 1.0f;
        round = 1;

        firstCard = null;
        secondCard = null;
        cardOpening = false;

        Init();
    }

    void Update()
    {
        //Game이 진행 중이고, 좀비 모드가 아닐 경우에만 시간이 흐르도록 설정
        if (isGameOver || Managers.Instance.gameType == GameLevel.Zombie)
        {
            return;
        }
        time -= Time.deltaTime * timeScale;

        //시간이 10초 이하 남았을 때, 음악이 변경되지 않았을 경우 BGM 멈추고 긴급한 BGM으로 교체
        if (time <= 10.0f && !isMusicChanged)
        {
            AudioManager.Instance.StopBGM();
            AudioManager.Instance.PlayHurryUpBGM();
            isMusicChanged = true;
        }

        //시간이 0초가 되었을 때 GameOver
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
                break;
        }
    }

    //카드 매칭 확인
    public void Matched() 
    {
        if (firstCard == null || secondCard == null)
        {
            return;
        }
        //카드 열리는 중
        cardOpening = true;
        //매칭 성공
        if (firstCard.idx == secondCard.idx)    
        {
            //무한 모드 - 5초 증가
            if (Managers.Instance.gameType == GameLevel.Infinite) 
            {
                //시간 추가
                time += 5.0f; 
                CardMatchEvent.Invoke();
            }
            //매칭 SFX 실행
            AudioManager.Instance.PlayMatchSFX();

            //카드 삭제
            firstCard.DestroyCard();
            secondCard.DestroyCard();

            //남은 카드 개수 줄이기
            cardCount -= 2;
            //카드 매칭 전부 성공
            if (cardCount == 0) 
            {
                //무한 모드인 경우
                if (Managers.Instance.gameType == GameLevel.Infinite)
                {
                    //카드 재배치
                    StartCoroutine(WaitAndShuffle()); 
                }
                else
                {
                    GameClear();
                }
            }
        }
        //매칭 실패
        else
        {
            AudioManager.Instance.PlayFailSFX();
            firstCard.CloseCard();
            secondCard.CloseCard();
            //좀비 모드인 경우 남은 뒤집기 횟수 줄이기
            if (Managers.Instance.gameType == GameLevel.Zombie) 
            {
                zombieCount--;
                //UI_Hp에서 hp가 줄어드는 함수 실행
                ZombieCountChanged.Invoke();
                //남은 뒤집기 횟수가 0이면 Game Over
                if (zombieCount == 0)
                {
                    GameOver();
                    return;
                }
                //좀비 모드 - 매칭 실패 시 카드 전부 활성화
                StartCoroutine(WaitAndActivate());  
            }
        }

        //0.5초 뒤, 카드 뒤집기 가능
        Invoke("AfterMatched", 0.5f);
    }

    //게임 시작
    public void GameStart() 
    {
        isGameOver = false;
        AudioManager.Instance.PlayNormalBGM(); ;
    }

    //게임 퍼즈
    public void GamePause() 
    {
        Time.timeScale = 0.0f;
        AudioManager.Instance.ControlBGM(false);
    }

    //퍼즈 해제
    public void GameContinue()  
    {
        Time.timeScale = 1.0f;
        AudioManager.Instance.ControlBGM(true);
    }

    //게임 멈춤
    public void GameStop()  
    {
        isGameOver = true;
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        isGameOver = true;
        //UI_MainScene > GameOver ui popup 함수 호출
        GameOverEvent.Invoke();

        //BGM 멈추기
        AudioManager.Instance.StopBGM();
        //GameOver SFX 실행
        AudioManager.Instance.PlayGameOverSFX();

        //무한 모드의 경우 GameOver==GameClear이기 때문에 점수 저장 필요함
        if (Managers.Instance.gameType == GameLevel.Infinite)
        {
            string typeKey = "InfiniteScore"; ;
            int roundCnt = (round < PlayerPrefs.GetInt(typeKey) ? PlayerPrefs.GetInt(typeKey) : round);
            PlayerPrefs.SetInt(typeKey, roundCnt);
        }
    }

    public void GameClear()
    {
        Time.timeScale = 0.0f;
        isGameOver = true;
        //UI_MainScene > GameClear ui popup 함수 호출
        GameClearEvent.Invoke();
        
        //BGM 멈추기
        AudioManager.Instance.StopBGM();

        //난이도, 모드마다 Score 저장
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
        else
        {
            if (typeKey == "ZombieScore")
            {
                PlayerPrefs.SetInt(typeKey, zombieCount);
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

    //카드가 뒤집히고 있는 중인지, 카드를 선택할 수 있는 상황인지 판단
    public bool CanSelectCard()
    {
        return !cardOpening && secondCard == null;
    }

    //매칭 후, 다시 카드 뒤집기 가능한 상태로 돌아감
    void AfterMatched() 
    {
        firstCard = null;
        secondCard = null;
        cardOpening = false;
    }

    IEnumerator WaitAndShuffle()
    {
        //카드 비활성화까지 대기
        yield return new WaitForSeconds(0.6f); 
        board.ShuffleCards();
        //라운드 + 1
        round += 1;
        //라운드가 증가할수록 시간 좀 더 빠르게 설정
        timeScale *= 1.2f; 
    }

    IEnumerator WaitAndActivate()
    {
        //카드 비활성화까지 대기
        yield return new WaitForSeconds(0.6f); 
        board.ActivateCards();
    }
}

