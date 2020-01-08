// Author: Bilal El Medkouri
// Co-Author: Joakim Ljung

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/Attack States/SweepState")]
public class GiantSweepState : GiantCombatState
{
    public override void Enter()
    {
        owner.SweepObject.SetActive(true);
        owner.Animator.SetTrigger("Sweep");

        base.Enter();
    }
}
