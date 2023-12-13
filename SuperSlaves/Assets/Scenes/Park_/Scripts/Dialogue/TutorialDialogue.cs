using System;
using UnityEngine;

public class TutorialDialogue : MonoBehaviour
{
    [SerializeField] private DialogueManager m_dialogueManager;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("TestDialogue", 1f);
    }

    private void TestDialogue()
    {
        m_dialogueManager.RunDialog("Dialogue_test");
    }
}
