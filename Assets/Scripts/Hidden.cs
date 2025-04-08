using UnityEngine;
using UnityEngine.SceneManagement;

public class Hidden : MonoBehaviour
{
    float direction = 0.05f;

    SpriteRenderer renderer;

    void Start()
    {
        Application.targetFrameRate = 60;
        renderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
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
}
