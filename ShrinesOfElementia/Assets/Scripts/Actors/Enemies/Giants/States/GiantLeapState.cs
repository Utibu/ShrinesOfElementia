// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/Attack States/LeapState")]
public class GiantLeapState : GiantCombatState
{
    public override void Enter()
    {
        MonoBehaviour.print("Leap");

        //owner.Animator.SetTrigger("Leap");

        base.Enter();
    }
}
