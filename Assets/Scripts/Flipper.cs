using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UIDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    //초기 Flipper 위치 저장
    private Vector2 originalPosition;
    private Coroutine returnCoroutine;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;
    }

    //드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Flipper가 현재 원래 위치로 돌아오고 있는 상태라면 returnCoroutine 중단
        if (returnCoroutine != null)
            StopCoroutine(returnCoroutine);
    }

    //드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        CheckCollision();
    }

    //드래그 끝
    public void OnEndDrag(PointerEventData eventData)
    {
        returnCoroutine = StartCoroutine(ReturnToOriginalPosition());
    }

    //충돌 확인
    void CheckCollision()
    {
        //현재 flipper의 rect 
        RectTransform thisRect = GetComponent<RectTransform>();

        //Hidden Tag를 가지고 있는 모든 오브젝트를 찾은 다음
        foreach (GameObject flipper in GameObject.FindGameObjectsWithTag("Hidden"))
        {
            //찾은 Hidden object의 rect transform과 현재 flipper의 rect transform이 겹쳐졌을 때
            RectTransform targetRect = flipper.GetComponent<RectTransform>();
            if (RectOverlaps(thisRect, targetRect))
            {
                //Hidden.cs에 존재하는 GotoHiddenStage 함수를 호출
                flipper.SendMessage("GotoHiddenStage", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    //두 개의 Rect Transform이 겹쳐져 있는지 확인
    bool RectOverlaps(RectTransform a, RectTransform b)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(b, a.position, canvas.worldCamera);
    }

    //원래 있던 위치로 이동
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
