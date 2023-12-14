using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[CreateAssetMenu(fileName = "New Move", menuName = "New Move")]
public class Movement : ScriptableObject
{
    [SerializeField] private List<Keys> m_moveKeyCodes;

    [field: SerializeField] public int ComboPriorty { get; private set; }
    [field: SerializeField] public Moves MoveType { get; private set; }

    [field: SerializeField] public PlayerTypes PlayerType { get; private set; }

    public bool IsMoveAvailable(List<Keys> pPlayerKeyCodes)
    {
        int comboIndex = 0;

        for (int i = 0; i < m_moveKeyCodes.Count && i < pPlayerKeyCodes.Count; i++)
        {
            if (pPlayerKeyCodes.Count <= i || m_moveKeyCodes.Count <= i)
            {
                break;
            }

            if (pPlayerKeyCodes[i] == m_moveKeyCodes[i])
            {
                comboIndex++;
                if (comboIndex == m_moveKeyCodes.Count)
                {
                    return true;
                }
            }
            else
            {
                comboIndex = 0;
            }
        }
        return false;
    }

    public int GetKeyCount()
    {
        return this.m_moveKeyCodes.Count;
    }

    public bool IsTryingCombo(List<Keys> pPlayerKeyCodes)
    {
        for(int i = 0; i < pPlayerKeyCodes.Count; i++)
        {
            if (pPlayerKeyCodes[i] != m_moveKeyCodes[i])
            {
                return false;
            }
        }
        return true;
    }
}

public enum Moves
{
    None,

    Jump,
    JumpingKick,

    Punch,
    Kick,
    Guard,

    Bend,
    BendedPunch,

    Skill,
}

public enum Keys
{
    None,
    Up,
    Down,
    Left,
    Right,
    Punch,
    Kick,
    Guard,
}

public enum PlayerTypes
{
    None,

    P01,
    P02,
    P03,
    P04,
    P05,
    P06,
    P07,
    P08,
    P09,
    P10,
    P11,
    P12,
    P13,
    P14,
    P15,
    P16,
    P17,
    P18,
    P19,
    P20,
    P21,
    P22,
    P23,
    P51,
}
