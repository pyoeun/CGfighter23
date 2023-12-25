using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSponer : MonoBehaviour
{
    [SerializeField] private Vector3 m_p1Pos;
    [SerializeField] private Vector3 m_p2Pos;

    private void Awake()
    {
        PlayerTypes p1Type = Main_single.player1;
        PlayerTypes p2Type = Main_single.player2;

        GameObject player1 = Resources.LoadAll<GameObject>("PlayerPrefabs/Player1").First(n => n.GetComponent<ControlManager>().PlayerType == p1Type);
        GameObject player2 = Resources.LoadAll<GameObject>("PlayerPrefabs/Player2").First(n => n.GetComponent<ControlManager>().PlayerType == p2Type);

        GameObject playerObj1 = GameObject.Instantiate(player1);
        playerObj1.transform.position = m_p1Pos;

        GameObject playerObj2 = GameObject.Instantiate(player2);
        playerObj2.transform.position = m_p2Pos;
    }
}
