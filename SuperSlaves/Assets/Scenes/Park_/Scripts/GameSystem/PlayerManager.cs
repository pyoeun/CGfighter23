using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private ControlManager m_p1;
    [SerializeField] private ControlManager m_p2;

    [SerializeField] private IngameManager m_ingameManager;
    [SerializeField] private GameObject m_gameManagerObj;

    private IGameManager m_gameManager;

    private void Awake()
    {
        if(m_ingameManager == null && m_gameManagerObj.GetComponent<IGameManager>() != null)
        {
            m_gameManager = m_gameManagerObj.GetComponent<IGameManager>();
        }
    }

    private void OnMoveP1(InputValue value)
    {
        Vector2 inputVec = value.Get<Vector2>();

        //Left
        if (inputVec.x < 0)
        {
            m_p1.AddKeys(Keys.Left);
            if(!m_p1.IsTouched || m_gameManager.Distance < m_p1.gameObject.transform.localScale.x / 2)
            {
                m_p1.transform.Translate(Vector3.left * m_p1.MoveSpeed);
            }
        }
        //Right
        if (inputVec.x > 0)
        {
            m_p1.AddKeys(Keys.Right);
            if(!m_p1.IsTouched || m_gameManager.Distance > m_p1.gameObject.transform.localScale.x / 2)
            {
                m_p1.transform.Translate(Vector3.right * m_p1.MoveSpeed);
            }
        }
        //Up
        if (inputVec.y > 0)
        {
            m_p1.AddKeys(Keys.Up);
            m_p1.GetComponent<PlayerController>().Jump();
        }
        //Down
        if (inputVec.y < 0)
        {
            m_p1.AddKeys(Keys.Down);
        }

        if(m_gameManagerObj != null)
        {
            m_gameManagerObj.GetComponent<TutorialGameManager>().SettingTutorialProgress(0, 1);
        }
    }

    private void OnPunchP1()
    {
        m_p1.AddKeys(Keys.Punch);
        if (m_gameManagerObj != null)
        {
            m_gameManagerObj.GetComponent<TutorialGameManager>().SettingTutorialProgress(1, 1);
        }
    }

    private void OnKickP1()
    {
        m_p1.AddKeys(Keys.Kick);
        if (m_gameManagerObj != null)
        {
            m_gameManagerObj.GetComponent<TutorialGameManager>().SettingTutorialProgress(2, 1);
        }
    }

    private void OnGuardP1()
    {
        m_p1.AddKeys(Keys.Guard);
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
            m_p2.AddKeys(Keys.Left);
            if ((!m_p2.IsTouched || m_gameManager.Distance > m_p2.gameObject.transform.localScale.x / 2))
            {
                m_p2.transform.Translate(Vector3.left * m_p2.MoveSpeed);
            }
        }
        //Right
        if (inputVec.x > 0)
        {
            m_p2.AddKeys(Keys.Right);
            if (!m_p2.IsTouched || m_gameManager.Distance < m_p2.gameObject.transform.localScale.x / 2)
            {
                m_p2.transform.Translate(Vector3.right * m_p2.MoveSpeed);
            }
        }
        //Up
        if (inputVec.y > 0)
        {
            m_p2.AddKeys(Keys.Up);
            m_p2.GetComponent<PlayerController>().Jump();
        }
        //Down
        if (inputVec.y < 0)
        {
            m_p2.AddKeys(Keys.Down);
        }

        if (m_gameManagerObj != null)
        {
            m_gameManagerObj.GetComponent<TutorialGameManager>().SettingTutorialProgress(0, 2);
        }
    }

    private void OnPunchP2()
    {
        m_p2.AddKeys(Keys.Punch);
        if (m_gameManagerObj != null)
        {
            m_gameManagerObj.GetComponent<TutorialGameManager>().SettingTutorialProgress(1, 2);
        }
    }

    private void OnKickP2()
    {
        m_p2.AddKeys(Keys.Kick);
        if (m_gameManagerObj != null)
        {
            m_gameManagerObj.GetComponent<TutorialGameManager>().SettingTutorialProgress(2, 2);
        }
    }

    private void OnGuardP2()
    {
        m_p2.AddKeys(Keys.Guard);
        if (m_gameManagerObj != null)
        {
            m_gameManagerObj.GetComponent<TutorialGameManager>().SettingTutorialProgress(3, 2);
        }
    }
}