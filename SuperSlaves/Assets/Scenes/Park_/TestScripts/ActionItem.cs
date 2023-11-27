using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionItem
{
    public enum InputAction
    {   Punch, 
        Kick, 
        Jump, 
    };

    public InputAction Action { get; private set; }
    public float TimeStamp { get; private set; }

    //�̰� ���� Ȯ���غ��� �ּ� �޾Ƶ־��� ��
    public static float TimeBeforeActionsExpire;

    public ActionItem(InputAction pInputAction, float pStamp)
    {
        Action = pInputAction;
        TimeStamp = pStamp;
    }

    public bool IsVaild()
    {
        if(TimeStamp + TimeBeforeActionsExpire >= Time.time)
        {
            return true;
        }

        return false;
    }
}
