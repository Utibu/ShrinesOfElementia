// Author: Bilal El Medkouri

using UnityEngine;

public class GiantCombatState : GiantBaseState
{
    [SerializeField] private int damage;

    public override void Enter()
    {
        owner.Animator.SetBool("IsAttacking", true);
    }

    public override void HandleUpdate()
    {
        if (owner.Animator.GetBool("IsAttacking") == false)
        {
            owner.Transition<GiantPhaseOneState>();
        }
    }
}