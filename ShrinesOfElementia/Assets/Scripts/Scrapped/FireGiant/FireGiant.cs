// Author: Bilal El Medkouri

using UnityEngine;

public class FireGiant : Giant
{
    // Fire Aura
    public bool FireAuraActive { get; set; }

    [Header("Fire Aura")]
    public int FireAuraDamage;
    [SerializeField] private float fireAuraCooldown;

    #region FireAuraCooldownVariables
    private float fireAuraTimer;
    private bool fireAuraAvailable;
    public bool FireAuraAvailable
    {
        get => fireAuraAvailable;
        set
        {
            fireAuraAvailable = value;

            if (value == false)
            {
                fireAuraTimer = fireAuraCooldown;
            }
        }
    }
    #endregion FireballCooldownVariables


    [Header("Fireball")]
    public int FireballDamage;
    public float FireballRange;
    [SerializeField] private float fireballCooldown;

    #region FireballCooldownVariables
    private float fireballTimer;
    private bool fireballAvailable;
    public bool FireballAvailable
    {
        get => fireballAvailable;
        set
        {
            fireballAvailable = value;

            if (value == false)
            {
                fireballTimer = fireballCooldown;
            }
        }
    }
    #endregion FireballCooldownVariables

    protected override void Awake()
    {
        base.Awake();

        FireAuraAvailable = true;
        FireballAvailable = false;
    }

    protected override void CountDownCooldowns()
    {
        base.CountDownCooldowns();

        // Fire Aura
        if (fireAuraAvailable == false && fireAuraTimer <= 0f)
        {
            FireAuraAvailable = true;
        }
        else if (fireAuraAvailable == false && fireAuraTimer > 0f)
        {
            fireAuraTimer -= Time.deltaTime;
        }

        // Fireball
        if (fireballAvailable == false && fireballTimer <= 0f)
        {
            FireballAvailable = true;
        }
        else if (fireballAvailable == false && fireballTimer > 0f)
        {
            fireballTimer -= Time.deltaTime;
        }
    }

    protected override void OnBossAreaEnter()
    {
        print("BossAreaEntered");
        Transition<FireGiantPhaseOneState>();
        BossEvents.Instance.OnBossFightAreaTriggerEnter -= OnBossAreaEnter;
    }
}
