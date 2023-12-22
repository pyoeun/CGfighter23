using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngameManager : MonoBehaviour, IGameManager
{
    public GameObject Player1 { get; set; }
    public GameObject Player2 { get; set; }

    [SerializeField] private TextMeshProUGUI m_IngameTimer;

    [SerializeField] private GameObject m_hitPrefab;
    [SerializeField] private GameObject m_defensePrefab;

    private float m_maxPlayTime = 60f;
    private float m_ingameTime;

    public float Distance { get; private set; }
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
        UpdateDistance();

        UpdateTimer();
    }

    public void UpdateDistance()
    {
        Distance = Player2.transform.position.x - Player1.transform.position.x;
        if (Distance * Sign < 0)
        {
            Sign *= -1;
            var scaleP1 = Player1.transform.localScale;
            scaleP1.x *= -1;
            Player1.transform.localScale = scaleP1;

            var scaleP2 = Player2.transform.localScale;
            scaleP2.x *= -1;
            Player2.transform.localScale = scaleP2;
        }
    }

    public void UpdateTimer()
    {
        m_ingameTime -= Time.deltaTime;
        m_IngameTimer.text = ((int)m_ingameTime).ToString();

        if (m_ingameTime <= 0)
        {
            //GameOver
            GameOver();
        }
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

    public void GameOver()
    {
        Time.timeScale = 0;

        var p1Life = Player1.GetComponent<PlayerLife>().LifeRate;
        var p2Life = Player2.GetComponent<PlayerLife>().LifeRate;

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
