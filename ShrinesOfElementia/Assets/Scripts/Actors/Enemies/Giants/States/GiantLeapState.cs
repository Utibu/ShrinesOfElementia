// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/Attack States/LeapState")]
public class GiantLeapState : GiantCombatState
{

    private float originalSpeed;

    public override void Enter()
    {
        MonoBehaviour.print("Leap");

        owner.Animator.SetTrigger("Leap");
        Debug.Log("ENTERING LEAP");


        originalSpeed = owner.Agent.speed;

        base.Enter();
    }


    public override void HandleUpdate()
    {
        base.HandleUpdate();
        owner.Agent.speed *=7;
        owner.Agent.SetDestination(Player.Instance.transform.position + Player.Instance.transform.forward * 4f);
    }

    public override void Leave()
    {
        base.Leave();
        owner.Agent.speed = originalSpeed;
        Debug.Log("LEAVING LEAP");
    }


    
}
