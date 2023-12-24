using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public ControlManager P1 { get; set; }
    public ControlManager P2 { get; set; }

    [SerializeField] private IngameManager m_ingameManager;
    [SerializeField] private GameObject m_gameManagerObj;

    private IGameManager m_gameManager;

    private Vector3 m_p1Direction;
    private Vector3 m_p2Direction;

    private void Awake()
    {
        if(m_ingameManager == null && m_gameManagerObj.GetComponent<IGameManager>() != null)
        {
            m_gameManager = m_gameManagerObj.GetComponent<IGameManager>();
        }
        else if(m_ingameManager != null)
        {
            m_gameManager = m_ingameManager;
        }
        else
        {
            Debug.LogError("GameManager Error");
        }
    }

    private void Update()
    {
        if (P1 != null && P2 != null)
        {
            P1.transform.Translate(TargetDirection(1, m_p1Direction) * P1.MoveSpeed * Time.deltaTime);
            P2.transform.Translate(TargetDirection(2, m_p2Direction) * P2.MoveSpeed * Time.deltaTime);
        }
    }

    private Vector3 TargetDirection(int pPlayerType, Vector3 pDirection)
    {
        Vector3 inputVec = pDirection;
        inputVec.y = 0;

        switch (pPlayerType)
        {
            case 1:
                if ((inputVec.x < 0 && m_gameManager.Distance >= P1.gameObject.transform.localScale.x / 2 && P1.IsTouched)
                    || (m_gameManager.Distance > 0 && !m_gameManager.IsAbletoMove(1)))
                {
                    inputVec.x = 0;
                }
                else if ((inputVec.x > 0 && m_gameManager.Distance <= P1.gameObject.transform.localScale.x / 2 && P1.IsTouched)
                    || (m_gameManager.Distance < 0 && !m_gameManager.IsAbletoMove(1)))
                {
                    inputVec.x = 0;
                }
                break;
            case 2:
                if ((inputVec.x < 0 && m_gameManager.Distance <= P2.gameObject.transform.localScale.x / 2 && P2.IsTouched)
                    || (m_gameManager.Distance < 0 && !m_gameManager.IsAbletoMove(2)))
                {
                    inputVec.x = 0;
                }
                else if ((inputVec.x > 0 && m_gameManager.Distance >= P2.gameObject.transform.localScale.x / 2 && P2.IsTouched)
                    || (m_gameManager.Distance > 0 && !m_gameManager.IsAbletoMove(2)))
                {
                    inputVec.x = 0;
                }
                break;
            default:
                Debug.LogError("그럴리가 없다.");
                break;
        }
        return inputVec;
    }


    private void OnMoveP1(InputValue value)
    {
        Vector2 inputVec = value.Get<Vector2>();

        //Left
        if (inputVec.x < 0)
        {
            P1.AddKeys(Keys.Left);
        }
        //Right
        if (inputVec.x > 0)
        {
            P1.AddKeys(Keys.Right);
        }
        //Up
        if (inputVec.y > 0)
        {
            P1.AddKeys(Keys.Up);
            P1.GetComponent<PlayerController>().Jump();
        }
        //Down
        if (inputVec.y < 0)
        {
            P1.AddKeys(Keys.Down);
        }

        if(m_gameManagerObj != null)
        {
            m_gameManagerObj.GetComponent<TutorialGameManager>().SettingTutorialProgress(0, 1);
        }

        m_p1Direction = inputVec;
    }

    private void OnPunchP1()
    {
        P1.AddKeys(Keys.Punch);
        if (m_gameManagerObj != null)
        {
            m_gameManagerObj.GetComponent<TutorialGameManager>().SettingTutorialProgress(1, 1);
        }
    }

    private void OnKickP1()
    {
        P1.AddKeys(Keys.Kick);
        if (m_gameManagerObj != null)
        {
            m_gameManagerObj.GetComponent<TutorialGameManager>().SettingTutorialProgress(2, 1);
        }
    }

    private void OnGuardP1()
    {
        P1.AddKeys(Keys.Guard);
        if (m_gameManagerObj != null)
        {
            m_gameManagerObj.GetComponent<TutorialGameManager>().SettingTutorialProgress(3, 1);
        }
    }

    private void OnMoveP2(InputValue value)
    {
        Vector2 inputVec = value.Get<Vector2>();

        //Left
        if (inputVec.x < 0)
        {
            P2.AddKeys(Keys.Left);
        }
        //Right
        if (inputVec.x > 0)
        {
            P2.AddKeys(Keys.Right);
        }
        //Up
        if (inputVec.y > 0)
        {
            P2.AddKeys(Keys.Up);
            P2.GetComponent<PlayerController>().Jump();
        }
        //Down
        if (inputVec.y < 0)
        {
            P2.AddKeys(Keys.Down);
        }

        if (m_gameManagerObj != null)
        {
            m_gameManagerObj.GetComponent<TutorialGameManager>().SettingTutorialProgress(0, 2);
        }

        m_p2Direction = inputVec;
    }

    private void OnPunchP2()
    {
        P2.AddKeys(Keys.Punch);
        if (m_gameManagerObj != null)
        {
            m_gameManagerObj.GetComponent<TutorialGameManager>().SettingTutorialProgress(1, 2);
        }
    }

    private void OnKickP2()
    {
        P2.AddKeys(Keys.Kick);
        if (m_gameManagerObj != null)
        {
            m_gameManagerObj.GetComponent<TutorialGameManager>().SettingTutorialProgress(2, 2);
        }
    }

    private void OnGuardP2()
    {
        P2.AddKeys(Keys.Guard);
        if (m_gameManagerObj != null)
        {
            m_gameManagerObj.GetComponent<TutorialGameManager>().SettingTutorialProgress(3, 2);
        }
    }
}
