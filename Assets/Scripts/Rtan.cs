using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Rtan : MonoBehaviour
{
    float direction = 0.1f;
    SpriteRenderer renderer;
    void Start()
    {
        Application.targetFrameRate = 60;
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 4.0f)
        {
            direction = -0.05f;
            renderer.flipX = true;
        }
        if (transform.position.x < -4.0f)
        {
            direction = 0.05f;
            renderer.flipX = false;
        }
        transform.position += Vector3.right * direction;
    }

}