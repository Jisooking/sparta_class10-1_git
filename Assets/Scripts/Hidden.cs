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
        if (transform.position.x > 2.6f) // 왼쪽으로 이동 
        {
            renderer.flipX = true;
            direction = -0.05f;
        }
        if (transform.position.x < -2.6f) // 오른쪽으로 이동
        {
            renderer.flipX = false;
            direction = 0.05f;
        }

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

        transform.position += Vector3.right * direction;
    }


}
