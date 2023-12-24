using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EndingScenePrinter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_huiwiObj;
    [SerializeField] private Sprite[] m_huiwi;

    [SerializeField] private TextMeshProUGUI m_winnerText;
    [SerializeField] private string[] m_texts;

    [SerializeField] private SpriteRenderer m_player1;
    [SerializeField] private SpriteRenderer m_player2;

    private GameObject m_winnerObj;

    private void Awake()
    {
        PlayerTypes p1Type = Main_single.player1;
        PlayerTypes p2Type = Main_single.player2;

        m_player1.sprite = Resources.LoadAll<SpriteRenderer>("/PlayerPrefs/Player1").First(n => n.GetComponent<ControlManager>().PlayerType == p1Type).sprite;
        m_player2.sprite = Resources.LoadAll<SpriteRenderer>("/PlayerPrefs/Player2").First(n => n.GetComponent<ControlManager>().PlayerType == p2Type).sprite;

        m_winnerText.text = m_texts[Main_single.Win];
        m_huiwiObj.sprite = m_huiwi[Main_single.Win];
        if(m_huiwiObj.sprite == null)
        {
            m_huiwiObj.color = Color.clear;
        }

        switch (Main_single.Win)
        {
            //P1
            case 0:
                m_winnerObj = m_player1.gameObject;
                InvokeRepeating("JumpWinner", 0f, 1f);
                break;
            //P2
            case 1:
                Vector3 targetScale = m_huiwiObj.transform.localScale;
                targetScale.x *= -1;
                m_huiwiObj.transform.localScale = targetScale;

                m_winnerObj = m_player2.gameObject;
                InvokeRepeating("JumpWinner", 0f, 1f);
                break;
             //Draw
            case 2:
                break;
            default:
                Debug.LogError("�׷���������.");
                break;

        }
    }

    private void JumpWinner()
    {
        m_winnerObj.transform.Translate(Vector3.up * 10);
    }
}
