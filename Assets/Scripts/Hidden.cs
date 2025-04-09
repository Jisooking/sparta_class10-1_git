using UnityEngine;
using UnityEngine.SceneManagement;

public class Hidden : MonoBehaviour
{
<<<<<<< HEAD
    void GotoHiddenStage()
    {
        Debug.Log("히든 스테이지로 이동합니다!");
        Managers.Instance.gameType = GameLevel.Hidden;
        LoadMainScene();
=======
    float direction = 0.05f;

    SpriteRenderer renderer;

    void Start()
    {
        Application.targetFrameRate = 60;
        renderer = GetComponent<SpriteRenderer>();

>>>>>>> parent of 2daeabb (Feat feat 스페셜 모드 선택 UI > 연결)
    }

    void Update()
    {
        SceneManager.LoadScene("MainScene");
    }

=======
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Managers.Instance.gameType = GameLevel.Hidden;
                SceneManager.LoadScene("MainScene");
                Debug.Log("히든 스테이지로");
            }
        }
    }
>>>>>>> parent of 2daeabb (Feat feat 스페셜 모드 선택 UI > 연결)
}
