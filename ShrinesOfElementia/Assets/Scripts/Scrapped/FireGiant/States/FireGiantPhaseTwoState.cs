// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/Fire Giant/PhaseTwoState")]
public class FireGiantPhaseTwoState : FireGiantPhaseOneState
{
    // This is all duplicate code. Needs to be removed/taken care of asap!

    public override void Enter()
    {
        owner.Agent.speed = movementSpeed;
        owner.Phase2Active = true;

        if (owner.HealthComponent.CurrentHealth <= (owner.HealthComponent.MaxHealth / 3))
        {
            owner.Transition<FireGiantPhaseThreeState>();
        }
    }
}
