using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Hidden : MonoBehaviour
{
<<<<<<< HEAD
    float direction = 0.05f;

    SpriteRenderer renderer;

    void Start()
=======
    void GotoHiddenStage()
>>>>>>> juho_branch
    {

        Debug.Log("히든 스테이지로 이동합니다!");
        Managers.Instance.gameType = GameLevel.Hidden;
        LoadMainScene();
    }

    void LoadMainScene()
    {
<<<<<<< HEAD
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
=======
        SceneManager.LoadScene("MainScene");
    }

>>>>>>> juho_branch
}
