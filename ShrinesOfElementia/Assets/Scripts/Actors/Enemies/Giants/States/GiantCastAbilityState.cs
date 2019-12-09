// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/Attack States/CastAbilityState")]
public class GiantCastAbilityState : GiantCombatState
{
    public override void Enter()
    {
        MonoBehaviour.print("Cast Ability");

        owner.Animator.SetTrigger("CastAbility");

        base.Enter();
    }
}
