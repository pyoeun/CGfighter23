using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(Button))]
[RequireComponent(typeof(CanvasGroup))]
public class TextButton : MonoBehaviour
{
    private Button m_button;
    private TextMeshProUGUI m_text;
    private CanvasGroup m_canvasGroup;

    public CanvasGroup CanvasGroup
    {
        get
        {
            if(m_canvasGroup == null)
                m_canvasGroup = GetComponent<CanvasGroup>();
            return m_canvasGroup;
        }
    }

    public Button Button
    {
        get
        {
            if(m_button == null)
                m_button = GetComponent<Button>();
            return m_button;
        }
    }

    public TextMeshProUGUI Text
    {
        get
        {
            if(m_text == null)
                m_text = GetComponentInChildren<TextMeshProUGUI>();
            return m_text;
        }
    }
}