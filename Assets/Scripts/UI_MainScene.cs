using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI_MainScene : MonoBehaviour
{
    public Text timeTxt;
    public Text roundTxt;
    public Text plusTimeText;

    //Zombie Mode에만 활성화
    public GameObject ui_Hp;

    //Special Mode에만 활성화
    public GameObject ui_DescriptionPopup;
    //Pause Button 클릭 시 활성화
    public GameObject ui_PausePopup;

    //GameClear or GameOver일 때 활성화
    public GameObject ui_SuccessPopup;
    public GameObject ui_FailPopup;
    //Infinite Mode일 때 게임이 끝나면 활성화
    public GameObject ui_InfiniteModeClearPopup;

    //Time 시각화 UI
    public Slider timeSlider;

    private void Start()
    {
        //GameOverEvent Invoke > PopupGameOver 함수 호출 
        GameManager.Instance.GameOverEvent -= PopupGameOver;
        GameManager.Instance.GameOverEvent += PopupGameOver;
        //GameClearEvent Invoke > PopupGameClear 함수 호출
        GameManager.Instance.GameClearEvent -= PopupGameClear;
        GameManager.Instance.GameClearEvent += PopupGameClear;
        //CardMatchEvent Invoke > PopupPlusTime 함수 호출
        GameManager.Instance.CardMatchEvent -= PopupPlusTime;
        GameManager.Instance.CardMatchEvent += PopupPlusTime;

        //Desctiption UI 활성화 > 각각의 Panel은 UI_Disable 함수가 들어가 있어 비활성화됨
        ui_DescriptionPopup.SetActive(true);
        //Pause UI 비활성화
        ui_PausePopup.SetActive(false);
        //Infinte Clear Popup 비활성화
        ui_InfiniteModeClearPopup.SetActive(false);

        switch (Managers.Instance.gameType)
        {
            //히든 모드: timeSlider 노출
            case GameLevel.Hidden:
                if (timeSlider != null)
                {
                    timeSlider.minValue = 0f;
                    timeSlider.maxValue = 1f;
                    timeSlider.value = 0f;
                    timeSlider.gameObject.SetActive(true);
                }
                break;
            //무한 모드: Round text 노출
            case GameLevel.Infinite:
                PopupRound();
                break;
            //좀비 모드: time 비활성화, Hp UI 활성화
            case GameLevel.Zombie:
                timeTxt.gameObject.SetActive(false);
                ui_Hp.SetActive(true);
                break;
            //쉬움, 일반, 어려움 모드: timeSlider 노출
            default:
                if (timeSlider != null)
                {
                    timeSlider.minValue = 0f;
                    timeSlider.maxValue = 1f;
                    timeSlider.value = 0f;
                    timeSlider.gameObject.SetActive(true);
                }
                break;
        }
    }
    void Update()
    {
        switch (Managers.Instance.gameType)
        {
            //히든 모드: timeText, timeSlider 
            case GameLevel.Hidden:
                timeTxt.text = GameManager.Instance._Time.ToString("N2");

                if (timeSlider != null && GameManager.Instance.maxtime > 0)
                {
                    float ratio = 1f - (GameManager.Instance._Time / GameManager.Instance.maxtime);
                    timeSlider.value = Mathf.Clamp01(ratio);
                }
                break;
            //무한 모드: timeText, roundText 
            case GameLevel.Infinite:
                timeTxt.text = GameManager.Instance._Time.ToString("N2");
                roundTxt.text = $"{GameManager.Instance._Round} Round";
                break;
            //좀비 모드: 
            case GameLevel.Zombie:
                break;
            //쉬움, 일반, 어려움: timeText, timeSlider
            default:
                timeTxt.text = GameManager.Instance._Time.ToString("N2");

                if (timeSlider != null && GameManager.Instance.maxtime > 0)
                {
                    float ratio = 1f - (GameManager.Instance._Time / GameManager.Instance.maxtime);
                    timeSlider.value = Mathf.Clamp01(ratio);
                }
                break;
        }
    }

    //GameOver UI 활성화
    void PopupGameOver()
    {
        //무한 모드일 경우 InfiniteModeClear UI 활성화 
        if (Managers.Instance.gameType == GameLevel.Infinite)
        {
            ui_InfiniteModeClearPopup.SetActive(true);
        }
        else
        {
            ui_FailPopup.SetActive(true);
        }
    }

    //GameSuccess UI 활성화
    void PopupGameClear()
    {
        ui_SuccessPopup.SetActive(true);
    }

    //RoundText 활성화
    void PopupRound()
    {
        roundTxt.gameObject.SetActive(true);
    }

    //PlusTimeText 활성화 
    void PopupPlusTime()
    {
        //애니메이션 재생을 위해 false 후 true인 듯? <<질문하기
        plusTimeText.gameObject.SetActive(false);
        plusTimeText.gameObject.SetActive(true);
    }

    //Pause UI 활성화, 게임 시간 흐르지 않게 GameStop 함수 실행
    public void OnClickPauseButton()
    {
        GameManager.Instance.GameStop();
        ui_PausePopup.SetActive(true);
    }
}

