using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameHitBox : MonoBehaviour
{
    [field : SerializeField] public PlayerLife Player { get; private set; }
    [field : SerializeField] public HitBox Type { get; private set; }
    [field : SerializeField] public float Power { get; private set; }   //����� ��� ����, ������ ���

    private List<IngameHitBox> hits = new List<IngameHitBox>();
    private Vector3 m_hitPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hitbox = collision.GetComponent<IngameHitBox>();

        if (hitbox != null && hitbox.Player != this.Player)
        {
            if(this.Type == HitBox.Attack)
            {
                hits.Add(hitbox);
                m_hitPos = this.transform.GetChild(0).position;
            }
        }
    }

    private void FixedUpdate()
    {
        if (hits.Count > 0)
        {
            bool isDefense = false;

            foreach (var hit in hits)
            {
                if (hit.Type == HitBox.Defense)
                {
                    isDefense = true;
                }
            }
            if (!isDefense)
            {
                hits[0].Player.UpdateLife(this.Power);
                FindObjectOfType<IngameManager>().Hit(m_hitPos);
            }
            else
            {
                FindObjectOfType<IngameManager>().Defense(m_hitPos);
                hits[0].Player.JostledEffect(600);
            }
        }

        hits.Clear();
    }
}

public enum HitBox
{
    Attack,
    Defense,
    Body,
}