// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceSystem : MonoBehaviour
{
    // Components
    public static ExperienceSystem Instance { get; private set; }

    [SerializeField] private Image experienceMeter;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Experience")]
    [SerializeField] private float currentExperience;
    [SerializeField] private float maxExperience;

    [Header("Level")]
    [SerializeField] private float currentLevel;
    [SerializeField] private float maxLevel;

    public float CurrentExperience
    {
        get => currentExperience;
        set
        {
            if (value > MaxExperience)
            {
                float excessExperience = value - MaxExperience;
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
            UpdateExperienceMeter();
        }
    }

    public float MaxExperience
    {
        get => maxExperience;
        set
        {
            maxExperience = value;
            //expBarController.MaxExperience = maxExperience;
        }
    }

    public float CurrentLevel
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
        //if (Instance == null) { Instance = this; }
        //else { Debug.Log("Warning: multiple " + this + " in scene!"); }

        CurrentExperience = 0;

        CurrentLevel = 1;

        MaxExperience = CalculateMaxExperience();

        UpdateExperienceMeter();

        UpdateLevelText();
    }
    private void Start()
    {
        EventManager.Instance.RegisterListener<ExperienceEvent>(ExperienceGained);

    }

    private float CalculateMaxExperience()
    {
        float newMaxExperience = 10 * CurrentLevel * CurrentLevel + 90;
        return newMaxExperience;
    }

    /* Byt till denna vid senare stadie, när vi har fått exp-values på fiender att scale:a med CurrentLevel
    private int CalculateMaxExperience()
    {
        int newMaxExperience = 10 * CurrentLevel * CurrentLevel + 90 * CurrentLevel;
        return newMaxExperience;
    }
    */

    //Gör till event av något slag
    private void LevelUp()
    {
        CurrentLevel++;
        EventManager.Instance.FireEvent(new LevelUpEvent("Level increased to: " + CurrentLevel));
        Player.Instance.Health.CurrentHealth = Player.Instance.Health.MaxHealth;
        MaxExperience = CalculateMaxExperience();
        UpdateLevelText();
    }

    private void ExperienceGained(ExperienceEvent experienceEvent)
    {
        print("Experience gained: " + experienceEvent.Experience);
        CurrentExperience += experienceEvent.Experience;
    }

    private void UpdateExperienceMeter()
    {
        print("updating experience");
        experienceMeter.fillAmount = CurrentExperience / MaxExperience;
    }

    private void UpdateLevelText()
    {
        levelText.text = "Level " + CurrentLevel;
    }
}
