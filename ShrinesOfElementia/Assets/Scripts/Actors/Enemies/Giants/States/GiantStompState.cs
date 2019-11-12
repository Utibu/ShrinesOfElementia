// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/Attack States/StompState")]
public class GiantStompState : GiantCombatState
{
    public override void Enter()
    {
        MonoBehaviour.print("Stomp");

        owner.Animator.SetTrigger("Stomp");

        base.Enter();
    }
}