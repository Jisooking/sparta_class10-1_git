using UnityEngine;

public class UI_DescriptionPopup : MonoBehaviour
{
    public GameObject hiddenPanel;
    public GameObject infinitePanel;
    public GameObject zombiePanel;

    private void Start()
    {
        //특별 모드의 경우, 설명 패널 팝업
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
