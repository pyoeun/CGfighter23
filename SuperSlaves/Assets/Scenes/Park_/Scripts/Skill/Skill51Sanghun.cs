using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skill51Sanghun : MonoBehaviour, ISkill
{
    public bool IsDebuffSkill { get; private set; }
    [SerializeField] private AnimationClip m_skillAnim;
    private IGameManager m_gameManager;
    private float m_timer;
    [SerializeField] private bool m_isP1 = true;
    [SerializeField] private float m_debuffTime;

    private GameObject m_target;
    [SerializeField] private GameObject m_skillObj;

    private void Awake()
    {
        m_gameManager = GameObject.Find("IngameManager").GetComponent<IGameManager>();
    }

    private void Start()
    {
        m_target = FindObjectsOfType<ControlManager>().FirstOrDefault(n => n.GetComponent<ISkill>() != this).gameObject;
    }

    public IEnumerator PlaySkill()
    {
        float animTime = m_skillAnim.length;
        m_timer = 0;

        IsDebuffSkill = true;

        while (m_timer < animTime)
        {
        m_skillObj.transform.position = m_target.transform.position;
            m_timer += Time.deltaTime;
            yield return null;
        }

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
