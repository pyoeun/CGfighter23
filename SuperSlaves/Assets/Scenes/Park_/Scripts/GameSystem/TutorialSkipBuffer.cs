using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TutorialSkipBuffer : MonoBehaviour
{
    [SerializeField] private Movement m_skipCommand;

    private List<Keys> m_pressedKeys = new List<Keys>();
    private float m_resetTime = 0.35f;

    private Coroutine m_timer;

    private void OnMoveP1(InputValue value)
    {
        Vector2 inputVec = value.Get<Vector2>();

        //Left
        if (inputVec.x < 0)
        {
            AddKey(Keys.Left);
        }
        //Right
        if (inputVec.x > 0)
        {
            AddKey(Keys.Right);
        }
    }

    private void OnPunchP1()
    {
        AddKey(Keys.Punch);
    }

    private void OnKickP1()
    {
        AddKey(Keys.Kick);
    }

    private void OnGuardP1()
    {
        AddKey(Keys.Guard);
    }

    private void AddKey(Keys key)
    {
        m_pressedKeys.Add(key);
        SetComboTimer();
    }

    private void SetComboTimer()
    {
        if (/*(!m_movementManager.CanMove(m_pressedKeys) && m_timer != null) || */m_timer != null)
        {
            StopCoroutine(m_timer);
        }

        m_timer = StartCoroutine(ResetComboTimer());
    }

    private IEnumerator ResetComboTimer()
    {
        yield return new WaitForSeconds(m_resetTime);

        if (m_skipCommand.IsMoveAvailable(m_pressedKeys))
        {
            //캐릭터 선택씬 이동
            Debug.Log("SkipTutorial");
            SceneManager.LoadScene("Ready");
        }

        m_pressedKeys.Clear();

        yield break;
    }
}
