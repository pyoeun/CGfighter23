using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill19Jun : MonoBehaviour, ISkill
{
    public bool IsDebuffSkill { get; private set; }
    [SerializeField] private AnimationClip m_skillAnim;
    private IGameManager m_gameManager;
    private float m_timer;
    [SerializeField] private bool m_isP1 = true;
    [SerializeField] private float m_debuffTime;
    [SerializeField] private float m_power;
    [SerializeField] private float m_moveTime = 0;

    private void Awake()
    {
        m_gameManager = GameObject.Find("IngameManager").GetComponent<IGameManager>();
        if(m_moveTime == 0)
        {
            m_moveTime = m_skillAnim.length / 3;
        }
    }

    public void Debuff()
    {
        if (m_isP1)
        {
            m_gameManager.IsAbleMoveP2 = false;
        }
        else
        {
            m_gameManager.IsAbleMoveP1 = false;
        }

        Invoke("ExitDebuff", m_debuffTime);
    }

    public IEnumerator PlaySkill()
    {
        float animTime = m_skillAnim.length;
        m_timer = 0;

        //m_gameManager.IsAbleMove = false;
        IsDebuffSkill = true;


        while (m_timer < animTime)
        {
            m_timer += Time.deltaTime;
            if (m_timer < m_moveTime)
            {
                this.transform.Translate((Mathf.Sign(this.transform.localScale.x) * Vector3.right * m_power) * Time.deltaTime);
            }
            yield return null;
        }

        //m_gameManager.IsAbleMove = true;
        IsDebuffSkill = false;

        yield break;
    }

    private void ExitDebuff()
    {
        if (m_isP1)
        {
            m_gameManager.IsAbleMoveP2 = true;
        }
        else
        {
            m_gameManager.IsAbleMoveP1 = true;
        }
    }
}
