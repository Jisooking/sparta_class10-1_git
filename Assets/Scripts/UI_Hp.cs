using UnityEngine;

public class UI_Hp : MonoBehaviour
{
    public GameObject[] hps;
    int idx;

    private void Start()
    {
        idx = 0;
        GameManager.Instance.ZombieCountChanged -= DiscoundHp;
        GameManager.Instance.ZombieCountChanged += DiscoundHp;
    }

    void DiscoundHp()
    {
        int count = GameManager.Instance.zombieCount;
        if (count % 2 == 0)
            hps[idx/2].GetComponent<Hp>().SetActiveEmpty();
        else
            hps[idx/2].GetComponent<Hp>().SetActiveHalf();
        idx++;
    }
}
