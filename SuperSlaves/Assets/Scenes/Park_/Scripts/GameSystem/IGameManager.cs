using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager
{
    public float Distance { get; }
    public bool IsAbleMoveP1 { get; set; }
    public bool IsAbleMoveP2 { get; set; }
    public void Hit(Vector3 pPos);
    public void Defense(Vector3 pPos);
    public void UpdateDistance();
    public void UpdateTimer();
    public void UpdateCamera();
    public bool IsAbletoMove(int pPlayerType);
}
