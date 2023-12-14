using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private List<Movement> m_availableMoves;
    private Movement m_skillMovement;
    private List<Movement> m_comboMovement = new List<Movement>();

    private void Awake()
    {
        m_availableMoves.Sort(Compare);
        foreach(var move in m_availableMoves)
        {
            if(move.MoveType == Moves.Skill)
            {
                m_skillMovement = move;
            }
        }

        foreach(var move in m_availableMoves)
        {
            if(move.GetKeyCount() > 1)
            {
                m_comboMovement.Add(move);
            }
        }
    }

    private int Compare(Movement move1, Movement move2)
    {
        return Comparer<int>.Default.Compare(move2.ComboPriorty, move1.ComboPriorty);
    }

    public bool IsTryingSkill(List<Keys> pKeyCodes)
    {
        if(m_skillMovement == null)
        {
            return false;
        }
        return m_skillMovement.IsTryingCombo(pKeyCodes);
    }

    public bool isTryingCombo(List<Keys> pKeyCodes, PlayerTypes pPlayerType)
    {
        bool result = false;

        foreach(var move in m_comboMovement)
        {
            if(move.MoveType == Moves.Skill && move.PlayerType != pPlayerType)
            {
                continue;
            }
            if (move.IsTryingCombo(pKeyCodes))
            {
                result = true;
                break;
            }
        }

        return result;
    }

    public bool CanMove(List<Keys> pKeyCodes)
    {
        foreach (var movement in m_availableMoves)
        {
            if (movement.IsMoveAvailable(pKeyCodes))
            {
                return true;
            }
        }
        return false;
    }

    public void PlayMove(List<Keys> pKeyCodes, PlayerController pPlayerController)
    {
        foreach (var movement in m_availableMoves)
        {
            if (movement.IsMoveAvailable(pKeyCodes))
            {
                pPlayerController.PlayerMove(movement.MoveType, movement.ComboPriorty);
                break;
            }
        }
    }
}
