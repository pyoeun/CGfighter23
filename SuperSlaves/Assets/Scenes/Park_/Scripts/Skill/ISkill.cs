using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    public bool IsDebuffSkill { get; }
    IEnumerator PlaySkill();
    void Debuff();
}
