using UnityEngine;
using UnityEngine.UI;

//UI 유틸리티. 확장메서드 형식으로 CanvasGroup을 보이게 하거나 숨길 수 있도록
public static class UIUtility
{
    //CanvasGroup을 보이게 하기 
    public static void Show(this CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    //CanvasGroup을 숨기기
    public static void Hide(this CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}