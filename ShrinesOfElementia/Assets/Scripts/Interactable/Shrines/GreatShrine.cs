using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatShrine : Shrine
{
    private bool isEnabled;
    private float bossesKilled;

    protected override void Start()
    {
        base.Start();
        EventManager.Instance.RegisterListener<BossDeathEvent>(OnBossDeath);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (isEnabled)
        {
            base.OnTriggerEnter(other);
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (isEnabled)
        {
            base.OnTriggerStay(other);
        }
        
    }



    protected override void OnInteract()
    {
        GameManager.Instance.EndGame();
    }

    private void OnBossDeath(BossDeathEvent eve)
    {
        if(bossesKilled >= 4)
        {
            isEnabled = true;
        }
    }
}
