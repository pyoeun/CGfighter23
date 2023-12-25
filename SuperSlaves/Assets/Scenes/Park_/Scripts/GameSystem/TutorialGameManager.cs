using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TutorialGameManager : MonoBehaviour, IGameManager
{
    public bool IsAbleMoveP1 { get; set; }
    public bool IsAbleMoveP2 { get; set; }

    [SerializeField] private GameObject m_player1;
    [SerializeField] private GameObject m_player2;

    [SerializeField] private TextMeshProUGUI m_IngameTimer;

    [SerializeField] private GameObject m_hitPrefab;
    [SerializeField] private GameObject m_defensePrefab;

    private Boolean[] m_p1Tutorial = new Boolean[System.Enum.GetValues(typeof(TutorialSeq)).Length];
    private Boolean[] m_p2Tutorial = new Boolean[System.Enum.GetValues(typeof(TutorialSeq)).Length];
    public Boolean[] IsPlayingTutorial { get; private set; }
    public Boolean[] IsReady { get; private set; }

    private Camera m_cam;
    private float m_minCamPos = -9.11f;
    private float m_maxCamPos = 9.11f;

    //private float m_maxPlayTime = 60f;
    //private float m_ingameTime;

    public float Distance { get; private set; }
    public int Sign { get; private set; }

    private void Awake()
    {
        m_cam = Camera.main;
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
        UpdateCamera();
    }

    public bool IsAbletoMove(int pPlayerType)
    {
        Vector3 pos1 = m_cam.WorldToViewportPoint(m_player1.transform.position);
        Vector3 pos2 = m_cam.WorldToViewportPoint(m_player2.transform.position);

        switch (pPlayerType)
        {
            case 1:
                if ((pos1.x < 0f && pos2.x >= 1f) || (pos1.x > 1f && pos2.x <= 0))
                {
                    Vector3 temp1 = new Vector3(Mathf.Clamp(pos1.x, 0, 1), pos1.y, pos1.z);
                    m_player1.transform.position = m_cam.ViewportToWorldPoint(temp1);

                    return false;
                }
                else if ((pos1.x <= 0f && m_cam.transform.position.x <= m_minCamPos) || (pos1.x >= 1f && m_cam.transform.position.x >= m_maxCamPos))
                {
                    return false;
                }
                return true;
            case 2:
                if ((pos1.x <= 0f && pos2.x > 1f) || (pos1.x >= 1f && pos2.x < 0))
                {
                    Vector3 temp2 = new Vector3(Mathf.Clamp(pos2.x, 0, 1), pos2.y, pos2.z);
                    m_player2.transform.position = m_cam.ViewportToWorldPoint(temp2);

                    return false;
                }
                else if ((pos2.x <= 0f && m_cam.transform.position.x <= m_minCamPos) || (pos2.x >= 1f && m_cam.transform.position.x >= m_maxCamPos))
                {
                    return false;
                }
                return true;
            default:
                Debug.LogError("그럴리가없다");
                return false;
        }
    }

    public void UpdateCamera()
    {
        if (IsAbletoMove(1) && IsAbletoMove(2))
        {
            m_cam.transform.position = new Vector3(Mathf.Clamp((m_player1.transform.position.x + m_player2.transform.position.x) / 2
                                                    , m_minCamPos, m_maxCamPos), 0, -10);
        }
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