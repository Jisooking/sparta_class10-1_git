using UnityEngine;
using UnityEngine.SceneManagement;
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
    //���̵� �ر�
    //playerpref ��� ������ ����(���� titlescene���� �Ѿ�� �� �ʱ�ȭ�ȴٸ�?)
    public bool unlockNormal;
    public bool unlockHard;

    //���̵� ���� ����
    public float easyScore;
    public float normalScore;
    public float hardScore;

    public static GameManager Instance;
    public Card firstCard;
    public Card secondCard;
    public Text timeTxt;
    float time;
    public float _Time
    {
        get { return time; }
        set { time = value; }
    }
    //public bool cardOpening = false;
    public int cardCount;
    public GameObject endTxt;
    

   
    public GameLevel gameType;

    public GameObject SuccessTxt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time < 0.0f)
        {
            time = 0.0f;

        }

        if (time >= 20.0f )
        {
            AudioManager.Instance.StopBGM();
            AudioManager.Instance.PlayHurryUpBGM();
            
        }
        

    }

    public void SetTime()
    {
        Time.timeScale = 1.0f;

        switch (gameType)
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
                time = 30.0f;
                break;
        }
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
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
        //Invoke("SetBoolFalse", 0.5f); // 카드가 뒤집었을 때 마우스 클릭 딜레이
        firstCard = null;
        secondCard = null;
    }

    public void GameOver()
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayGameOverSFX();
        float score = time;
        string typeKey = "";
        //���̵��� ���� �ر� ����, ���� ����
        switch (gameType)
        {
            case GameLevel.Easy:
                unlockNormal = true;
                //����
                typeKey = "EasyScore";
                break;
            case GameLevel.Normal:
                unlockHard = true;
                typeKey = "NormalScore";
                break;
            case GameLevel.Hard:
                typeKey = "HardScore";
                break;
        }

        //���� ����
        if (PlayerPrefs.HasKey(typeKey))
            score = (score < PlayerPrefs.GetFloat(typeKey) ? PlayerPrefs.GetFloat(typeKey) : score);
        PlayerPrefs.SetFloat(typeKey, score);

        Debug.Log($"{typeKey}: {PlayerPrefs.GetFloat(typeKey)}");

        endTxt.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void GameClear()
    {
        SuccessTxt.SetActive(true);
        Time.timeScale = 0.0f;

    }

    /*void SetBoolFalse()
    {
        cardOpening = false;
    }*/
}

