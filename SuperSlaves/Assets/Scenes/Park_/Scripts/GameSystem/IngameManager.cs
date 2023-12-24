using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameManager : MonoBehaviour, IGameManager
{
    public bool IsAbleMove { get; set; }
    public GameObject Player1 { get; set; }
    public GameObject Player2 { get; set; }

    [SerializeField] private TextMeshProUGUI m_IngameTimer;

    [SerializeField] private GameObject m_hitPrefab;
    [SerializeField] private GameObject m_defensePrefab;

    private float m_maxPlayTime = 60f;
    private float m_ingameTime;

    private Camera m_cam;
    private float m_minCamPos = -9.11f;
    private float m_maxCamPos = 9.11f;

    public float Distance { get; private set; }
    public int Sign { get; private set; }

    private void Awake()
    {
        Sign = 1;
        IsAbleMove = true;
        m_cam = Camera.main;
    }

    private void Start()
    {
        m_ingameTime = m_maxPlayTime;
    }

    private void Update()
    {
        UpdateDistance();
        UpdateCamera();
        UpdateTimer();
    }

    public bool IsAbletoMove(int pPlayerType)
    {
        Vector3 pos1 = m_cam.WorldToViewportPoint(Player1.transform.position);
        Vector3 pos2 = m_cam.WorldToViewportPoint(Player2.transform.position);

        switch (pPlayerType)
        {
            case 1:
                if((pos1.x < 0f && pos2.x >= 1f) || (pos1.x > 1f && pos2.x <= 0f))
                {
                    Vector3 temp1 = new Vector3(Mathf.Clamp(pos1.x, 0, 1), pos1.y, pos1.z);
                    Player1.transform.position = m_cam.ViewportToWorldPoint(temp1);

                    return false;
                }
                else if((pos1.x <= 0f && m_cam.transform.position.x <= m_minCamPos) || (pos1.x >= 1f && m_cam.transform.position.x >= m_maxCamPos))
                {
                    return false;
                }
                return true;
            case 2:
                if ((pos1.x <= 0f && pos2.x > 1f) || (pos1.x >= 1f && pos2.x < 0f))
                {
                    Vector3 temp2 = new Vector3(Mathf.Clamp(pos2.x, 0, 1), pos2.y, pos2.z);
                    Player2.transform.position = m_cam.ViewportToWorldPoint(temp2);

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
            m_cam.transform.position = new Vector3(Mathf.Clamp((Player1.transform.position.x + Player2.transform.position.x) / 2
                                                    , m_minCamPos, m_maxCamPos), 0, -10);
        }
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
        //Time.timeScale = 0;

        var p1Life = Player1.GetComponent<PlayerLife>().LifeRate;
        var p2Life = Player2.GetComponent<PlayerLife>().LifeRate;

        if(p1Life > p2Life)
        {
            //player1 won!
            Debug.Log("Winner is... P1!");
            Main_single.Win = 0;
        }
        else if(p1Life < p2Life)
        {
            //player2 won!
            Debug.Log("Winner is... P2!");
            Main_single.Win = 1;
        }
        else
        {
            //draw...!
            Debug.Log("Draw!");
            Main_single.Win = 2;
        }
        Invoke("ShowEnding", 1.5f);
    }

    private void ShowEnding()
    {
        SceneManager.LoadScene("EndingScene");
    }
}
