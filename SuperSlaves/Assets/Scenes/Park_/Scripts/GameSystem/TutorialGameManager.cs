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

    //private float m_maxPlayTime = 60f;
    //private float m_ingameTime;

    public float Distance { get; private set; }
    public int Sign { get; private set; }

    private void Awake()
    {
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
}
