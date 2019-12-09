// Author: Bilal El Medkouri

using UnityEngine;

public class GiantAbilityManager : MonoBehaviour
{
    public static GiantAbilityManager Instance { get; private set; }

    [SerializeField] private GameObject giantAbility = null;
    [Range(0f, 100f)] public float AbilityRange = 0f;
    [Range(0f, 60f)] [SerializeField] protected float abilityCooldown = 0f;

    private float abilityTimer = 0f;

    private bool abilityAvailable = true;
    public bool AbilityAvailable
    {
        get => abilityAvailable;
        set
        {
            abilityAvailable = value;

            if (value == false)
            {
                abilityTimer = abilityCooldown;
            }
        }
    }

    protected virtual void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }
    }

    public void CountDownCooldown()
    {
        if (AbilityAvailable == false)
        {
            if (abilityTimer > 0f)
            {
                abilityTimer -= Time.deltaTime;
            }
            else if (abilityTimer <= 0f)
            {
                AbilityAvailable = true;
            }
        }
    }
}
