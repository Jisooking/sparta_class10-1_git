using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UIDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalPosition;
    private Coroutine returnCoroutine;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (returnCoroutine != null)
            StopCoroutine(returnCoroutine);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        CheckCollision();

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        returnCoroutine = StartCoroutine(ReturnToOriginalPosition());
    }

    void CheckCollision()
    {
        RectTransform thisRect = GetComponent<RectTransform>();

        foreach (GameObject flipper in GameObject.FindGameObjectsWithTag("Hidden"))
        {
            RectTransform targetRect = flipper.GetComponent<RectTransform>();
            if (RectOverlaps(thisRect, targetRect))
            {
                flipper.SendMessage("GotoHiddenStage", SendMessageOptions.DontRequireReceiver);
            }
        }

    }

    bool RectOverlaps(RectTransform a, RectTransform b)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(b, a.position, canvas.worldCamera);
    }

    IEnumerator ReturnToOriginalPosition()
    {
        Vector2 startPos = rectTransform.anchoredPosition;
        float duration = 0.3f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float progress = t / duration;
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, originalPosition, progress);
            yield return null;
        }

        rectTransform.anchoredPosition = originalPosition;
    }
}
