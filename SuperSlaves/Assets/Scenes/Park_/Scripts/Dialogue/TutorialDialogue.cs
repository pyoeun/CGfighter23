using System;
using System.Collections;
using UnityEngine;

public class TutorialDialogue : MonoBehaviour
{
    [SerializeField] private DialogueManager m_dialogueManager;
    [SerializeField] private String m_platform = "PC";
    [SerializeField] private CanvasGroup m_dialogueCanvas;
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("TestDialogue", 1f);
        StartCoroutine(PlayTutorial());
    }

    void StartDialogue(String pLuaName)
    {
        m_dialogueManager.RunDialog(pLuaName);
    }

    private IEnumerator PlayTutorial()
    {
        yield return new WaitForFixedUpdate();
        m_dialogueManager.RunDialog($"{m_platform}MoveTutorials");
        yield return new WaitUntil(() => m_dialogueCanvas.alpha == 0);
        yield return new WaitForSeconds(3f);
        m_dialogueManager.RunDialog($"Dialogue_test");
        yield break;
    }
}
