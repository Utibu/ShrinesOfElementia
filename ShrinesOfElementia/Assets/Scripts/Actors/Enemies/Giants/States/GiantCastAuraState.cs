// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/CastAuraState")]
public class GiantCastAuraState : GiantCombatState
{
    public override void Enter()
    {
        MonoBehaviour.print("Cast Aura");

        owner.Animator.SetTrigger("CastAura");

        base.Enter();
    }
}
