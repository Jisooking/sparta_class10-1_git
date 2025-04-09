using UnityEngine;

public class UI_DescriptionPopup : MonoBehaviour
{
    public GameObject hiddenPanel;
    public GameObject infinitePanel;
    public GameObject zombiePanel;

    private void Start()
    {
        //Ư�� ����� ���, ���� �г� �˾�
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
