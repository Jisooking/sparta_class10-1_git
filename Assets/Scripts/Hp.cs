using UnityEngine;

public class Hp : MonoBehaviour
{
    public GameObject hp_Half;
    public GameObject hp_Empty;

    void Start()
    {
        //꽉찬 하트 활성화를 위한 나머지 하트 이미지 비활성화
        hp_Half.SetActive(false);
        hp_Empty.SetActive(false);
    }

    //반틈 하트 활성화
    public void SetActiveHalf()
    {
        hp_Half.SetActive(true);
    }

    //비어 있는 하트 활성화
    public void SetActiveEmpty()
    {
        hp_Empty.SetActive(true);
    }
}