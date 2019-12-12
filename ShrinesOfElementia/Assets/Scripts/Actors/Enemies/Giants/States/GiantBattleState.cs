// Author: Bilal El Medkouri

public class GiantBattleState : GiantBaseState
{


    public override void HandleUpdate()
    {
        // Aura
        /*if(GiantAuraManager.AuraEnabled == false && GiantAuraManager.AuraAvailable == true)
        {
            GiantAuraManager.Instance.AuraAvailable = false;
            owner.Transition<GiantCastAuraState>();
        }*/

        // Ability
        /*
        if (GiantAbilityManager.Instance.AbilityAvailable == true && GiantAbilityManager.Instance.AbilityRange >= owner.DistanceToPlayer())
        {
            GiantAbilityManager.Instance.AbilityAvailable = false;
            owner.Transition<GiantCastAbilityState>();
        }
        */

        


        // Sweep
        if (owner.SweepAvailable && owner.SweepRange >= owner.DistanceToPlayer())
        {
            owner.SweepAvailable = false;
            owner.Transition<GiantSweepState>();
        }

        // Stomp
        else if (owner.StompAvailable && owner.StompRange >= owner.DistanceToPlayer() && owner.DistanceToPlayer() >= owner.StompRange /2)  // owner.Phase2Active &&  
        {
            owner.StompAvailable = false;
            owner.Transition<GiantStompState>();
        }

        //reactivate passive
        else if (owner.GetComponent<GiantPassiveManager>().IsReady && owner.GetComponent<GiantPassiveManager>().instantiatedPrefab == null)
        {
            owner.Transition<GiantCastAuraState>();
        }

        // Basic Attack, punch
        else if (owner.BasicAttackRange >= owner.DistanceToPlayer())
        {
            owner.Transition<GiantAttackState>();
        }

        //Leap
        else if ( owner.LeapAvailable && owner.LeapRange >= owner.DistanceToPlayer())  // owner.Phase3Active &&
        {
            owner.LeapAvailable = false;
            owner.Transition<GiantLeapState>();
        }

        //cast ability
        else if (owner.GiantAbilityManagerRef.Ready && owner.GiantAbilityManagerRef.AbilityRange >= owner.DistanceToPlayer() && owner.DistanceToPlayer() >= owner.GiantAbilityManagerRef.AbilityMinRange)
        {
            owner.Transition<GiantCastAbilityState>();
        }

        

        // Chase
        else
        {
            owner.Transition<GiantChaseState>();
        }

        
        
        

    }






}
