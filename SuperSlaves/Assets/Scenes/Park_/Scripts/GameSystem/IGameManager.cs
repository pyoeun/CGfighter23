using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager
{
    public float Distance { get; }
    public void Hit(Vector3 pPos);
    public void Defense(Vector3 pPos);
    public void UpdateDistance();
    public void UpdateTimer();
}
