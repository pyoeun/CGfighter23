using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialGameManager : MonoBehaviour, IGameManager
{
    [SerializeField] private GameObject m_player1;
    [SerializeField] private GameObject m_player2;

    [SerializeField] private TextMeshProUGUI m_IngameTimer;

    [SerializeField] private GameObject m_hitPrefab;
    [SerializeField] private GameObject m_defensePrefab;

    private Boolean[] m_p1Tutorial = new Boolean[System.Enum.GetValues(typeof(TutorialSeq)).Length];
    private Boolean[] m_p2Tutorial = new Boolean[System.Enum.GetValues(typeof(TutorialSeq)).Length];
    public Boolean[] IsPlayingTutorial { get; private set; }
    public Boolean[] IsReady { get; private set; }

    //private float m_maxPlayTime = 60f;
    //private float m_ingameTime;

    public float Distance { get; private set; }
    public int Sign { get; private set; }

    private void Awake()
    {
        IsPlayingTutorial = new Boolean[System.Enum.GetValues(typeof(TutorialSeq)).Length];
        IsReady = new Boolean[System.Enum.GetValues(typeof(TutorialSeq)).Length];
        for (int i = 0; i < System.Enum.GetValues(typeof(TutorialSeq)).Length; i++)
        {
            IsReady[i] = false;
            IsPlayingTutorial[i] = false;
            m_p1Tutorial[i] = false;
            m_p2Tutorial[i] = false;
        }
        Sign = 1;
    }

    private void Start()
    {
        UpdateTimer();
    }

    private void Update()
    {
        UpdateDistance();
    }

    public void UpdateDistance()
    {
        Distance = m_player2.transform.position.x - m_player1.transform.position.x;
        if (Distance * Sign < 0)
        {
            Sign *= -1;
            var scaleP1 = m_player1.transform.localScale;
            scaleP1.x *= -1;
            m_player1.transform.localScale = scaleP1;

            var scaleP2 = m_player2.transform.localScale;
            scaleP2.x *= -1;
            m_player2.transform.localScale = scaleP2;
        }
    }

    public void UpdateTimer()
    {
        m_IngameTimer.text = "XX";
    }

    public void Hit(Vector3 pPos)
    {
        var hit = Instantiate(m_hitPrefab);
        hit.transform.position = pPos;
    }

    public void Defense(Vector3 pPos)
    {
        var def = Instantiate(m_defensePrefab);
        def.transform.position = pPos;
    }

    public void SettingTutorialProgress(int pSeq, int pPlayerType)
    {
        if (!IsPlayingTutorial[pSeq])
        {
            return;
        }
        try
        {
            if(pPlayerType <= 1)
            {
                m_p1Tutorial[pSeq] = true;
            }
            else
            {
                m_p2Tutorial[pSeq] = true;
            }

            if (m_p1Tutorial[pSeq] && m_p2Tutorial[pSeq])
            {
                IsReady[pSeq] = true;
            }
        }
        catch(Exception e)
        {
            Debug.LogError(e);
        }
    }
}

public enum TutorialSeq
{
    Move,
    Punch,
    Kick,
    Guard,
}