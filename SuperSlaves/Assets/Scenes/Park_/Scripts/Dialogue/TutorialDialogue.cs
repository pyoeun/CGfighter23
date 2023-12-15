using System;
using System.Collections;
using UnityEngine;

public class TutorialDialogue : MonoBehaviour
{
    [SerializeField] private DialogueManager m_dialogueManager;
    [SerializeField] private String m_platform = "PC";
    [SerializeField] private CanvasGroup m_dialogueCanvas;
    [SerializeField] private TutorialGameManager m_tutorialGameManager;
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
        m_tutorialGameManager.IsPlayingTutorial[0] = true;
        yield return new WaitUntil(() => m_tutorialGameManager.IsReady[0]);
        yield return new WaitForSeconds(0.5f);

        m_dialogueManager.RunDialog($"{m_platform}PunchTutorial");
        m_tutorialGameManager.IsPlayingTutorial[1] = true;
        yield return new WaitUntil(() => m_tutorialGameManager.IsReady[1]);
        yield return new WaitForSeconds(0.5f);

        m_dialogueManager.RunDialog($"{m_platform}KickTutorial");
        m_tutorialGameManager.IsPlayingTutorial[2] = true;
        yield return new WaitUntil(() => m_tutorialGameManager.IsReady[2]);
        yield return new WaitForSeconds(0.5f);

        m_dialogueManager.RunDialog($"{m_platform}GuardTutorial");
        yield break;
    }
}
