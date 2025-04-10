using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PausePopup : MonoBehaviour
{
    //돌아가기 버튼 클릭 시 다시 게임 시간 흐르도록 GameStart 함수 실행
    public void OnClickBackButton()
    {
        GameManager.Instance.GameStart();
        gameObject.SetActive(false);
    }

    public void OnClickMainButton()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void OnClickExitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
