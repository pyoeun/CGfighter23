using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

using DialogueSystem;

public class DialogueUI : MonoBehaviour, IDialogueInput, IDialogueOutput
{
    [SerializeField] private TextMeshProUGUI m_talkLineText;
    [SerializeField] private TextMeshProUGUI m_talkerNameText;
    [SerializeField] private Image m_illustImage;
    [SerializeField] private TextButton[] m_selectionButtons;

    private CanvasGroup m_canvasGroup;

    private String m_talkLine;
    private String m_talkerName;
    private String m_illustPath;
    private String[] m_selections;


    private Int32 m_lastIndex;
    private Coroutine m_printRoutine;
    private Boolean m_isPrinting;
    private Boolean m_isAddingRichTextTag;


    private void Awake()
    {
        m_lastIndex = -1;
        m_canvasGroup = GetComponent<CanvasGroup>();
        m_canvasGroup.Hide();
    }


    public int ReadSelection()
    {
        return m_lastIndex;
    }


    public void WriteLine(string pLine)
    {
        m_talkLine = pLine;
    }

    public void WriteTalkerName(string pTalkerName)
    {
        m_talkerName = pTalkerName;
    }

    public void WriteSelections(string[] pSelections)
    {
        m_selections = pSelections;
    }

    public void WriteIllust(string pIllust)
    {
        m_illustPath = pIllust;
    }

    public void BeginPrint()
    {
        m_canvasGroup.Show();
        HideAllButtons();
    }

    public void DoPrint(Action pNext)
    {
        if (m_printRoutine != null)
            StopCoroutine(m_printRoutine);

        m_printRoutine = StartCoroutine(PrintRoutine(pNext));
    }

    public void EndPrint()
    {
        m_canvasGroup.Hide();
    }


    private void HideAllButtons()
    {
        foreach (var button in m_selectionButtons)
        {
            button.CanvasGroup.Hide();
        }
    }

    private void OnSelect(Int32 pIndex)
    {
        m_lastIndex = pIndex;
        HideAllButtons();
    }

    private void Update()
    {
        if (!m_isPrinting)
            return;


        if (Input.GetKeyDown(KeyCode.Space))
            m_isPrinting = false;
    }

    private IEnumerator PrintRoutine(Action pNext)
    {
        m_talkLineText.text = "";
        m_talkerNameText.text = m_talkerName;
        m_illustImage.sprite = Resources.Load<Sprite>($"Illust/{m_illustPath}");
        m_isAddingRichTextTag = false;

        if (m_selections == null)
        {
            m_isPrinting = true;
            foreach (var ch in m_talkLine)
            {
                m_talkLineText.text += ch;
                if(ch == '<' || m_isAddingRichTextTag)
                {
                    m_isAddingRichTextTag = true;
                    if(ch == '>')
                    {
                        m_isAddingRichTextTag = false;
                    }
                    continue;
                }
                yield return new WaitForSeconds(0.08f);

                if(!m_isPrinting)
                {
                    m_talkLineText.text = m_talkLine;
                    break;
                }
            }
            m_isPrinting = false;

            //다 출력하면 스페이스바를 눌러 다음으로
            yield return new WaitUntil(() => Input.anyKeyDown);
            pNext.Invoke();
        }
        else
        {
            m_talkLineText.text = m_talkLine;
            for(int i=0;i<m_selections.Length;i++)
            {
                Int32 index = i; //이거 반드시 필요함!! 람다식 캡쳐 내부에 그냥 i를 쓰면 문제 생김!!
                m_selectionButtons[i].CanvasGroup.Show();
                m_selectionButtons[i].Text.text = m_selections[i];
                m_selectionButtons[i].Button.onClick.RemoveAllListeners();
                m_selectionButtons[i].Button.onClick.AddListener(() => OnSelect(index)); //여기 주의!!!!! 
                m_selectionButtons[i].Button.onClick.AddListener(new UnityAction(pNext));
            }
            m_selections = null;
        }
    }

}
