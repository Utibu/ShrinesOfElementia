//"author" Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBarController : HealthBarController
{
    public static  HealthBarController Instance { get; private set; }
    public GameObject bossHealthBar;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EventManager.Instance.RegisterListener<BossDeathEvent>(OnBossDeath);
    }

    public void OnBossFightTrigger()
    {
        bossHealthBar.SetActive(true);
        InitiateHealthValues();
    }

    private void OnBossDeath(BossDeathEvent ev)
    {
        bossHealthBar.SetActive(false);
    }
}
