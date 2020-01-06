// Author: Bilal El Medkouri

using UnityEngine;

public class GiantCombatState : GiantBaseState
{
    [SerializeField] private int damage;

    public override void Enter()
    {
        owner.transform.LookAt(Player.Instance.transform);
        owner.Animator.SetBool("IsAttacking", true);
    }

    public override void HandleUpdate()
    {
        //turn towards palyer
        Vector3 direction = (Player.Instance.transform.position - owner.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, lookRotation, Time.deltaTime * 3f);

        if (owner.Animator.GetBool("IsAttacking") == false)
        {
            owner.Transition<GiantPhaseOneState>();
        }
    }
}