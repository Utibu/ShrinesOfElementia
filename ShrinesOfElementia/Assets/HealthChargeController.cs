//Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthChargeController : MonoBehaviour
{
    [SerializeField] private Image firstHealthImage;
    [SerializeField] private Image secondHealthImage;
    [SerializeField] private Image thirdHealthImage;
    [SerializeField] private Image[] healthImages;

    private void Awake()
    {
        EventManager.Instance.RegisterListener<PlayerDeathEvent>(OnPlayerDeath);
    }

    private void Start()
    {
        foreach(Image healthImage in healthImages)
        {
            healthImage.enabled = true;
        }
    }

    private void OnPlayerDeath(PlayerDeathEvent eve)
    {
        foreach(Image healthImage in healthImages)
        {
            if (healthImage.IsActive())
            {
                healthImage.enabled = false;
                return;
            }
        }
        /*
        if(firstHealthImage.IsActive())
        {
            firstHealthImage.enabled = false;
        }
        else if (secondHealthImage.IsActive())
        {
            secondHealthImage.enabled = false;
        }
        else if (thirdHealthImage.IsActive())
        {
            secondHealthImage.enabled = false;
        }
        */
    }
}
