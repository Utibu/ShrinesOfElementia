// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/Fire Giant/PhaseThreeState")]
public class FireGiantPhaseThreeState : FireGiantPhaseTwoState
{
    // This is all duplicate code. Needs to be removed/taken care of asap!

    public override void Enter()
    {
        owner.Phase3Active = true;
        owner.Agent.speed = movementSpeed;
    }
}
