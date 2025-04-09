using UnityEngine;

public class Hp : MonoBehaviour
{
    public GameObject hp_Half;
    public GameObject hp_Empty;

    void Start()
    {
        hp_Half.SetActive(false);
        hp_Empty.SetActive(false);
    }

    public void SetActiveHalf()
    {
        hp_Half.SetActive(true);
    }

    public void SetActiveEmpty()
    {
        hp_Empty.SetActive(true);
    }
}
