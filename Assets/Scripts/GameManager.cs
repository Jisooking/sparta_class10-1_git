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
    public Button NormalBtn;
    public Button HardBtn;

    //���̵� ���� ����
    public float easyScore;
    public float normalScore;
    public float hardScore;

    public static GameManager Instance;
    public Card firstCard;
    public Card secondCard;
    public Text timeTxt;
    float time;
    public int cardCount;
    public GameObject endTxt;
    public AudioSource audioSource;
    public AudioClip clip;  //매칭 성공 사운드

    public AudioClip failClip;  //매칭 실패 사운드

    public AudioClip gameOverClip; //게임오버 사운드

    public AudioSource bgm; //배경음악 재생 소스

    public AudioClip hurryUpClip;   //긴박한 배경음악

    bool isMusicChanged = false;

    public GameLevel gameType;

    public GameObject SuccessTxt;

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

        //���̵��� ���� ���� �ð� ���ϱ�
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
        }
        if (GameManager.Instance.unlockNormal)
        {
            NormalBtn.interactable = true;
        }
        else { NormalBtn.interactable = false; }
        if (GameManager.Instance.unlockHard)
        {
            HardBtn.interactable = true;
        }
        else { HardBtn.interactable = false; }
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



        if (time >= 20.0f && !isMusicChanged) //20초 지나면 긴박한 브금 재생
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
                GameClear();
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

    public void LoadEasy()
    {
        GameManager.Instance.gameType = GameLevel.Easy;
        SceneManager.LoadScene("MainScene");

    }
    public void LoadNormal()
    {
        GameManager.Instance.gameType = GameLevel.Normal;
        SceneManager.LoadScene("MainScene");
    }
    public void LoadHard()
    {
        GameManager.Instance.gameType = GameLevel.Hard;
        SceneManager.LoadScene("MainScene");
    }

    public void GameClear()
    {
        SuccessTxt.SetActive(true);
        Time.timeScale = 0.0f;

    }
}

