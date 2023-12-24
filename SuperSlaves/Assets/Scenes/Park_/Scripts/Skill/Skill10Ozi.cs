using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill10Ozi : MonoBehaviour, ISkill
{
    public bool IsDebuffSkill { get { return false; } }
    //카메라 절반 정도의 크기(양수)
    [SerializeField] private float m_camSize = 11;

    [SerializeField] private AnimationClip m_skillAnim;
    private IGameManager m_gameManager;
    private float m_timer;
    [SerializeField] private short m_playerSign = 1;

    private void Awake()
    {
        m_gameManager = GameObject.Find("IngameManager").GetComponent<IGameManager>();
    }

    public IEnumerator PlaySkill()
    {
        float animTime = m_skillAnim.length;
        float standardSign = Mathf.Sign(this.transform.localScale.x) * m_playerSign;
        Vector3 defaultPos = this.transform.position;
        Vector3 standardPosition = new Vector3(m_camSize, -1, defaultPos.z);
        m_timer = 0;
        //m_gameManager.IsAbleMove = false;

        while(m_timer < animTime)
        {
            m_timer += Time.deltaTime;
            this.transform.position = Vector3.Lerp(new Vector3(-standardSign * standardPosition.x, standardPosition.y)
                                                , new Vector3(standardSign * standardPosition.x, standardPosition.y)
                                                , m_timer / animTime);
            yield return null;
        }

        //m_gameManager.IsAbleMove = true;

        yield break;
    }
    public void Debuff()
    {

    }
}
