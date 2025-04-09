using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData) { } // �ִϸ��̼��̳� ���带 ���� �� ����

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; //���콺 �����̴� �Ÿ�
        CheckCollision();
    }

    void CheckCollision()
    {
        RectTransform thisRect = GetComponent<RectTransform>();


        foreach (GameObject flipper in GameObject.FindGameObjectsWithTag("Hidden"))
        {
            RectTransform targetRect = flipper.GetComponent<RectTransform>();
            if (RectOverlaps(thisRect, targetRect))
            {
                Debug.Log("���� ����������");
                flipper.SendMessage("GotoHiddenStage", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    bool RectOverlaps(RectTransform a, RectTransform b)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(b, a.position, canvas.worldCamera);
    }
}