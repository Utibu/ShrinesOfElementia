// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceSystem : MonoBehaviour
{
    // Components
    public static ExperienceSystem Instance { get; private set; }

    [Header("Experience")]
    [SerializeField] private int currentExperience;
    [SerializeField] private int maxExperience;

    [Header("Level")]
    [SerializeField] private int currentLevel;
    [SerializeField] private int maxLevel;

    // Bygg ut alla properties så att de faktiskt gör något vid get och set. Åtminstone set!
    public int CurrentExperience
    {
        get => currentExperience;
        set
        {
            if (value > MaxExperience)
            {
                int excessExperience = value - MaxExperience;
                LevelUp();
                CurrentExperience = excessExperience;
            }
            else if (value == MaxExperience)
            {
                LevelUp();
                currentExperience = 0;
            }
            else
            {
                currentExperience = value;
            }

            //expBarController.CurrentExperience = currentExperience;
        }
    }

    public int MaxExperience
    {
        get => maxExperience;
        set
        {
            maxExperience = value;
            //expBarController.MaxExperience = maxExperience;
        }
    }

    public int CurrentLevel
    {
        get => currentLevel;
        set
        {
            currentLevel = value;
            //someUIThing.CurrentLevel = currentLevel;
        }
    }

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }

        CurrentExperience = 0;

        CurrentLevel = 1;

        MaxExperience = CalculateMaxExperience();
    }

    private int CalculateMaxExperience()
    {
        int newMaxExperience = 10 * CurrentLevel * CurrentLevel + 90;
        return newMaxExperience;
    }

    /* Byt till denna vid senare stadie, när vi har fått exp-values på fiender att scale:a med CurrentLevel
    private int CalculateMaxExperience()
    {
        int newMaxExperience = 10 * CurrentLevel * CurrentLevel + 90 * CurrentLevel;
        return newMaxExperience;
    }
    */

    private void LevelUp()
    {
        //Gör till event av något slag
        Player.Instance.Health.CurrentHealth = Player.Instance.Health.MaxHealth;
        CurrentLevel++;
        MaxExperience = CalculateMaxExperience();
    }
}
