using UnityEngine;
using UnityEngine.UI;
public class UI_MainScene : MonoBehaviour
{
    public Text timeTxt; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeTxt.text = GameManager.Instance._Time.ToString("N2");
    }
}
