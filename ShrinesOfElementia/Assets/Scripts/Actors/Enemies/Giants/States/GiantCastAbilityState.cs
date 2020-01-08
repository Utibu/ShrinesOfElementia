// Author: Bilal El Medkouri
// Co-Author: Sofia Chyle Kauko

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

    public override void HandleUpdate()
    {
        //temporary solution to the aim at player problem. 
        owner.transform.LookAt(Player.Instance.transform);
        base.HandleUpdate();
        
    }
}
