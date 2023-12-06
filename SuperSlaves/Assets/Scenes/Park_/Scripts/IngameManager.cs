using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_player1;
    [SerializeField] private GameObject m_player2;

    [SerializeField] private TextMeshProUGUI m_IngameTimer;

    [SerializeField] private GameObject m_hitPrefabs;

    private float m_maxPlayTime = 60f;
    private float m_ingameTime;

    public float Distance;
    public int Sign { get; private set; }

    private void Awake()
    {
        Sign = 1;
    }

    private void Start()
    {
        m_ingameTime = m_maxPlayTime;
    }

    private void Update()
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

        m_ingameTime -= Time.deltaTime;
        m_IngameTimer.text = ((int)m_ingameTime).ToString();
        if(m_ingameTime <= 0)
        {
            //GameOver
            GameOver();
        }
    }

    public void Hit(Vector3 pPos)
    {
        var hit = Instantiate(m_hitPrefabs);
        hit.transform.position = pPos;
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        var p1Life = m_player1.GetComponent<PlayerLife>().LifeRate;
        var p2Life = m_player2.GetComponent<PlayerLife>().LifeRate;

        if(p1Life > p2Life)
        {
            //player1 won!
            Debug.Log("Winner is... P1!");
        }
        else if(p1Life < p2Life)
        {
            //player2 won!
            Debug.Log("Winner is... P2!");
        }
        else
        {
            //draw...!
            Debug.Log("Draw!");
        }
    }
}
