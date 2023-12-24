using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill03Ajin : MonoBehaviour, ISkill
{
    [SerializeField] private AnimationClip m_skillAnim;
    private float m_timer;
    private IGameManager m_gameManager;

    private void Awake()
    {
        m_gameManager = GameObject.Find("IngameManager").GetComponent<IGameManager>();
    }

    public IEnumerator PlaySkill()
    {
        float animTime = m_skillAnim.length;
        m_timer = 0;

        //m_gameManager.IsAbleMove = false;

        while (m_timer < animTime)
        {
            m_timer += Time.deltaTime;
            yield return null;
        }

        //m_gameManager.IsAbleMove = true;

        yield break;
    }
}
