using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill06Chani : MonoBehaviour, ISkill
{
    public bool IsDebuffSkill { get; private set; }
    [SerializeField] private AnimationClip m_skillAnim;
    private IGameManager m_gameManager;
    private float m_timer;
    [SerializeField] private bool m_isP1 = true;
    [SerializeField] private float m_debuffTime;

    private void Awake()
    {
        m_gameManager = GameObject.Find("IngameManager").GetComponent<IGameManager>();
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
            yield return null;
        }

        //m_gameManager.IsAbleMove = true;
        IsDebuffSkill = false;

        yield break;
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
