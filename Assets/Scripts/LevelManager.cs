using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Card firstCard;
    public Card secondCard;
    public Text timeTxt;
    float time;
    //public bool cardOpening = false;
    public int cardCount;
    public GameObject endTxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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

        if (time <= 10.0f)
        {
            AudioManager.Instance.StopBGM();
            AudioManager.Instance.PlayHurryUpBGM();

        }

    }
}
