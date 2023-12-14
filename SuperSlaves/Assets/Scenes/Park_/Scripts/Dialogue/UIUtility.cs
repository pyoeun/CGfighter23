using UnityEngine;
using UnityEngine.UI;

//UI ��ƿ��Ƽ. Ȯ��޼��� �������� CanvasGroup�� ���̰� �ϰų� ���� �� �ֵ���
public static class UIUtility
{
    //CanvasGroup�� ���̰� �ϱ� 
    public static void Show(this CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    //CanvasGroup�� �����
    public static void Hide(this CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}