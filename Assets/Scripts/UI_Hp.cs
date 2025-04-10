using UnityEngine;

public class UI_Hp : MonoBehaviour
{
    public GameObject[] hps;
    //몇 번째 하트 선택할지 확인하기 위한 인덱스 변수
    int idx;

    private void Start()
    {
        idx = 0;
        //GameManager.Instance.zombieCount 변할 때마다 이벤트 호출
        GameManager.Instance.ZombieCountChanged -= DiscountHp;
        GameManager.Instance.ZombieCountChanged += DiscountHp;
    }

    //zombieCount의 횟수가 줄어들 때마다 하트 이미지 변화
    void DiscountHp()
    {
        int count = GameManager.Instance.zombieCount;
        if (count % 2 == 0)
            hps[idx/2].GetComponent<Hp>().SetActiveEmpty();
        else
            hps[idx/2].GetComponent<Hp>().SetActiveHalf();
        idx++;
    }
}
