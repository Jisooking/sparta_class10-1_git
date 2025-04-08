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
        float x = transform.position.x;
        float y = transform.position.y;
        if (x > -2.6f)
        {
            transform.position = new Vector2(-0.05f,0);
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
