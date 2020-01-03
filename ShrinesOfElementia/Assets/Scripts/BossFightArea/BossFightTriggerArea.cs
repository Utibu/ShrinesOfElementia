﻿// Author: Bilal El Medkouri
//co-Author: Sofia Kauko

using UnityEngine;

public class BossFightTriggerArea : MonoBehaviour
{

    [SerializeField] private GameObject bossHealthBar;
    [SerializeField] private GameObject boss;

    /*  gav null references jag inte pallar felsöka. 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("BossFightTriggered");
            BossEvents.Instance.BossFightAreaTriggerEnter();
        }
    }

    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bossHealthBar.SetActive(true);
            boss.GetComponent<Giant>().StartBattle();
            //Giant.Instance.StartBattle();
            
        }
    }


}
