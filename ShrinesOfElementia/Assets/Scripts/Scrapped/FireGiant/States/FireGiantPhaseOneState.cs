// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/Fire Giant/PhaseOneState")]
public class FireGiantPhaseOneState : FireGiantBattleState
{
    // This is all duplicate code. Needs to be removed/taken care of asap!

    [SerializeField] protected float movementSpeed;

    public override void Enter()
    {
        owner.Agent.speed = movementSpeed;

        if (owner.HealthComponent.CurrentHealth <= ((owner.HealthComponent.MaxHealth * 2) / 3))
        {
            owner.Transition<GiantPhaseTwoState>();
        }
    }
}
