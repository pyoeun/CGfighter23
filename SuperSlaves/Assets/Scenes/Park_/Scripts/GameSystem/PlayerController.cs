using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator m_animator;
    private ControlManager m_controlManager;
    private Rigidbody2D m_rigid;
    private int m_currentComboPriorty = 0;
    private bool m_canJump = false;
    public bool IsOnGround { get; private set; }

    private void Awake()
    {
        if (m_animator == null)
        {
            m_animator = this.GetComponent<Animator>();
        }
        if (m_controlManager == null)
        {
            m_controlManager = this.GetComponent<ControlManager>();
        }
        if(m_rigid == null)
        {
            m_rigid = this.GetComponent<Rigidbody2D>();
        }

        IsOnGround = false;
    }

    public void PlayerMove(Moves pMove, int pComboPriorty)
    {
        if (pMove != Moves.None)
        {
            if (pComboPriorty >= m_currentComboPriorty)
            {
                m_currentComboPriorty = pComboPriorty;
                ResetTriggers();
            }
            else
            {
                return;
            }

            //Set Animation
            switch (pMove)
            {
                case Moves.Jump:
                    if (m_canJump)
                    {
                        m_animator.SetTrigger("Jump");
                        //Jump();
                    }
                    break;
                case Moves.JumpingKick:
                    if (m_canJump)
                    {
                        m_animator.SetTrigger("JumpingKick");
                        //Jump();
                    }
                    break;

                case Moves.Punch:
                    m_animator.SetTrigger("Punch");
                    break;
                case Moves.Kick:
                    m_animator.SetTrigger("Kick");
                    break;
                case Moves.Guard:
                    m_animator.SetTrigger("Guard");
                    break;

                case Moves.Bend:
                    m_animator.SetTrigger("Bend");
                    break;
                case Moves.BendedPunch:
                    m_animator.SetTrigger("BendedPunch");
                    break;

                case Moves.Skill:
                    if (m_controlManager.CanSkill)
                    {
                        m_animator.SetTrigger("Skill");
                        Debug.Log("Skill~!!!");
                        m_controlManager.PlaySkill();
                    }
                    break;
                default:
                    break;
            }

            m_currentComboPriorty = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            IsOnGround = true;
            StartCoroutine(BanJump());
        }
        if(collision.gameObject.tag == "Player")
        {
            if (!IsOnGround)
            {
                StartCoroutine(IgnorePlayer(collision.gameObject));
            }
        }
    }

    public void Jump()
    {
        if (IsOnGround)
        {
            m_rigid.AddForce(Vector3.up * m_controlManager.JumpPower, ForceMode2D.Impulse);
            IsOnGround = false;
        }
    }

    private void ResetTriggers()
    {
        foreach (var parameter in m_animator.parameters)
        {
            m_animator.ResetTrigger(parameter.name);
        }
    }

    private IEnumerator BanJump()
    {
        m_canJump = false;
        yield return new WaitForSeconds(m_controlManager.ComboResetTime);
        m_canJump = true;
        yield break;
    }

    private IEnumerator IgnorePlayer(GameObject pPlayer)
    {
        while (!IsOnGround)
        {
            pPlayer.layer = 3;
            yield return null;
        }
        pPlayer.layer = 0;
    }
}
