using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBarController : MonoBehaviour
{
    [SerializeField] private Image experienceMeter;
    private int currentStamina;
    
    public int CurrentExperience { get { return currentStamina; } set { currentStamina = value; UpdateExperienceMeter(); } }

    private void Start()
    {
        experienceMeter.fillAmount = 0;
    }

    private void UpdateExperienceMeter()
    {
        experienceMeter.fillAmount = CurrentExperience / 100;
    }
}
