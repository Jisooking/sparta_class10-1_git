using UnityEngine;

public class testbtn : MonoBehaviour
{
    public void OnClickTestBtn()
    {
        GameManager.Instance.GameClear();
    }
}
