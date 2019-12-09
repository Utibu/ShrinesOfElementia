// Author: Bilal El Medkouri

using UnityEngine;

[System.Serializable]
public class GiantAbility_Scrapped
{
    public string AbilityName = "New Ability";
    public GameObject AbilityObject = null;
    [Range(0f, 300f)] public int Damage = 0;
    [Range(0f, 100f)] public float Range = 0f;
    [Range(0f, 60f)] public float Cooldown = 0f;

    public enum UnlockedAtPhase { One, Two, Three };
    public UnlockedAtPhase unlockedAtPhase = UnlockedAtPhase.One;

    public enum EntersState { GiantAttackState, GiantSweepState, GiantStompState, GiantLeapState, GiantCastAuraState, GiantCastAbilityState }
    public EntersState entersState = EntersState.GiantAttackState;

    public float AbilityTimer { get; set; } = 0f;

    private bool abilityAvailable = true;
    public bool AbilityAvailable
    {
        get => abilityAvailable;
        set
        {
            abilityAvailable = value;

            if (value == false)
            {
                AbilityTimer = Cooldown;
            }
        }
    }

    // Maybe put this in GiantAbilityManager.cs instead
    public void CountDownCooldown()
    {
        if (AbilityAvailable == false)
        {
            if (AbilityTimer > 0f)
            {
                AbilityTimer -= Time.deltaTime;
            }
            else if (AbilityTimer <= 0f)
            {
                AbilityAvailable = true;
            }
        }
    }

    public void CastAbilityCheck()
    {
        if (PhaseActive() == true)
        {
            if (AbilityAvailable == true)
            {
                if (WithinRange() == true)
                {
                    CastAbility();
                }
            }
        }
    }

    private bool PhaseActive()
    {
        if (unlockedAtPhase == UnlockedAtPhase.One)
        {
            return true;
        }
        else
        {
            bool phaseActive = unlockedAtPhase == UnlockedAtPhase.Two ? Giant.Instance.Phase2Active : Giant.Instance.Phase3Active;
            if (phaseActive == true)
            {
                return true;
            }
        }
        return false;
    }

    public bool WithinRange()
    {
        if (Range >= Giant.Instance.DistanceToPlayer())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void CastAbility()
    {
        AbilityAvailable = false;

        // Override and do more in sub-classes
    }
}
