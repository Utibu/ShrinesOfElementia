// Author: Bilal El Medkouri

using UnityEngine;

public class FireGiantBattleState : GiantBattleState
{
    // DOESN'T WORK!
    // Starting my work on the ability manager


    // Temporary fix. After breaking out all of the giant's abilities, this should be taken care of as well
    protected new FireGiant owner;
    public override void Initialize(StateMachine owner)
    {
        this.owner = (FireGiant)owner;
    }

    public override void HandleUpdate()
    {
        // Fire Aura
        if (owner.FireAuraActive == false && owner.FireAuraAvailable == true)
        {
            owner.FireAuraAvailable = false;
            MonoBehaviour.print("Fire Aura");
            //owner.Transition<FireGiantCastFireAuraState>();
        }

        // Fireball
        else if (owner.FireballAvailable == true && owner.FireballRange >= owner.DistanceToPlayer())
        {
            owner.FireballAvailable = false;
            MonoBehaviour.print("Fireball");
            //owner.Transition<FireGiantFireballState>();
        }

        // Base Giant Abilities
        else
        {
            base.HandleUpdate();
        }
    }
}
