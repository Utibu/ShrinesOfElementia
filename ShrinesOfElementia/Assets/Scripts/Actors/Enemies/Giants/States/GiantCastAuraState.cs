// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/CastAuraState")]
public class GiantCastAuraState : GiantCombatState
{
    public override void Enter()
    {
        MonoBehaviour.print("Cast Aura");

        owner.Agent.isStopped = true;
        owner.Animator.SetBool("IsAttacking", false);
        owner.Animator.SetTrigger("CastAura");

        base.Enter();
    }

    public override void Leave()
    {
        owner.Agent.isStopped = false;
        base.Leave();
    }
}
