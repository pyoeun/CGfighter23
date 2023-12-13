using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System;
using Unity.VisualScripting;

public class ControlManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_textForTest;
    [SerializeField] private String m_playerName;
    [SerializeField] private PlayerTypes m_playerType;
    [SerializeField] private float m_skillCooltime;
    [SerializeField] private Slider m_skillSlider;

    [field : SerializeField] public List<Keys> PressedKeys { get; private set; }
    [field : SerializeField] public float ComboResetTime { get; private set; }
    [field: SerializeField] public float JumpPower { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }

    private MovementManager m_movementManager;
    private PlayerController m_playerController;

    public bool IsTouched { get; private set; }
    public bool CanSkill { get; private set; }

    private Coroutine m_timer;
    private float m_skillTimer;

    private void Awake()
    {
        if (m_movementManager == null)
        {
            m_movementManager = FindObjectOfType<MovementManager>();
        }
        if (m_playerController == null)
        {
            m_playerController = this.GetComponent<PlayerController>();
        }

        IsTouched = false;
        m_skillTimer = m_skillCooltime;
    }

    private void Update()
    {
        if(m_skillTimer < m_skillCooltime)
        {
            m_skillTimer += Time.deltaTime;
            ShowCoolTime();
        }
        else
        {
            CanSkill = true;
        }
        PrintControls();
    }

    public void AddKeys(Keys key)
    {
        PressedKeys.Add(key);
        if (m_movementManager.isTryingCombo(PressedKeys, m_playerType))
        {
            SetComboTimer();
        }
        else
        {
            m_movementManager.PlayMove(PressedKeys, m_playerController);
            PressedKeys.Clear();

            if ((!m_movementManager.CanMove(PressedKeys) && m_timer != null) || m_timer != null)
            {
                StopCoroutine(m_timer);
            }
        }
    }

    public void SetComboTimer()
    {
        if (/*(!m_movementManager.CanMove(m_pressedKeys) && m_timer != null) || */m_timer != null)
        {
            StopCoroutine(m_timer);
        }

        m_timer = StartCoroutine(ResetComboTimer());
    }

    public IEnumerator ResetComboTimer()
    {
        yield return new WaitForSeconds(ComboResetTime);

        m_movementManager.PlayMove(PressedKeys, m_playerController);
        PressedKeys.Clear();

        yield break;
    }

    public void PlaySkill()
    {
        m_skillTimer = 0;
        CanSkill = false;
    }

    public void PrintControls()
    {
        m_textForTest.text = $"{m_playerName} Buffer : ";

        foreach (Keys keyCode in PressedKeys)
        {
            m_textForTest.text += $"{keyCode} ";
        }
    }

    private void ShowCoolTime()
    {
        m_skillSlider.value = m_skillTimer / m_skillCooltime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<ControlManager>() != null)
        {
            IsTouched = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ControlManager>() != null)
        {
            IsTouched = false;
        }
    }
}
