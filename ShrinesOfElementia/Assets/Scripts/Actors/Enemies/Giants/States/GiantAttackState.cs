﻿// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/Attack States/AttackState")]
public class GiantAttackState : GiantCombatState
{
    public override void Enter()
    {
        owner.BasicObject.SetActive(true);
        owner.Animator.SetTrigger("Attack");

        base.Enter();
    }
}
