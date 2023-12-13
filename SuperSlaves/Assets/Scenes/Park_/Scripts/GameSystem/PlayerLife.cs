using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private float MaxLife;
    [SerializeField] private float CurrentLife;
    //[SerializeField] private TextMeshProUGUI m_lifeText;
    [SerializeField] private Slider m_lifeSlider;
    [SerializeField] private GameObject m_gameManagerObj;
    [SerializeField] private bool m_isP1;

    private IngameManager m_ingameManager;
    private IGameManager m_gameManager;

    public float LifeRate { get { return CurrentLife / MaxLife; } }
    private PlayerController m_playerController;

    private float m_hitSpeed = 2400;
    private float m_guardSpeed = 600;

    private void Awake()
    {
        if(m_playerController == null)
        {
            m_playerController = this.GetComponent<PlayerController>();
        }
        if(m_gameManagerObj != null && m_gameManager == null)
        {
            m_gameManager = m_gameManagerObj.GetComponent<IGameManager>();   
        }
        if(m_ingameManager == null && m_gameManagerObj == null)
        {
            m_ingameManager = FindObjectOfType<IngameManager>();
        }

        CurrentLife = MaxLife;
        PrintLife();
    }

    public void UpdateLife(float value)
    {
        if(value <= 0)
        {
            return;
        }

        CurrentLife -= value;
        if (CurrentLife <= 0 && m_ingameManager != null)
        {
            m_ingameManager.GameOver();
        }

        JostledEffect(m_hitSpeed);

        PrintLife();
    }

    public void JostledEffect(float pSpeed)
    {
        if(m_gameManager == null)
        {
            return;
        }
        var rigid = this.gameObject.GetComponent<Rigidbody2D>();
        rigid.AddForce(new Vector2(m_gameManager.Distance > 0 ? -1 : 1, 0) * (m_isP1? 1 : -1) * pSpeed);
    }

    private void PrintLife()
    {
        m_lifeSlider.value = CurrentLife;
    }
}
