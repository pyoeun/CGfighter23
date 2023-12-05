using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameHitBox : MonoBehaviour
{
    [field : SerializeField] public PlayerLife Player { get; private set; }
    [field : SerializeField] public HitBox Type { get; private set; }
    [field : SerializeField] public float Power { get; private set; }   //����� ��� ����, ������ ���

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hitbox = collision.GetComponent<IngameHitBox>();
        if (hitbox != null && hitbox.Player != this.Player)
        {
            if(this.Type == HitBox.Attack)
            {
                if(hitbox.Type == HitBox.Attack)
                {
                    //�׳� ���� �ɷ� ħ... �� ��.
                    hitbox.Player.UpdateLife(this.Power);
                }
                else
                {
                    //Power ���� ���ؼ� ���ݸ��� �ɷ� ħ
                    hitbox.Player.UpdateLife(Mathf.Clamp(this.Power + hitbox.Power, 0, this.Power));
                }
            }
        }
    }
}

public enum HitBox
{
    Attack,
    Defense,
    Body,
}