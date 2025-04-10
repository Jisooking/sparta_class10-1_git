using UnityEngine;

public class UI_DescriptionPopup : MonoBehaviour
{
    public GameObject hiddenPanel;
    public GameObject infinitePanel;
    public GameObject zombiePanel;

    private void Start()
    {
        //스페셜 모드일 때 게임 타입에 따라 Panel 다르게 보여 주기
        if (Managers.Instance.gameType == GameLevel.Hidden)
        {
            hiddenPanel.SetActive(true);
        }
        else if (Managers.Instance.gameType == GameLevel.Infinite)
        {
            infinitePanel.SetActive(true);
        }
        else if (Managers.Instance.gameType == GameLevel.Zombie)
        {
            zombiePanel.SetActive(true);
        }
    }
}
