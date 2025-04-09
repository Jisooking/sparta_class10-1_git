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

    public void OnBeginDrag(PointerEventData eventData) { } // 애니메이션이나 사운드를 넣을 수 있음

    public void OnDrag(PointerEventData eventData) 
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; //마우스 움직이는 거리
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
                Debug.Log("히든 스테이지로");
                flipper.SendMessage("GotoHiddenStage", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    bool RectOverlaps(RectTransform a, RectTransform b)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(b, a.position, canvas.worldCamera);
    }
}
