//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{

    [SerializeField] private GameObject[] bosses = new GameObject[4];

    [SerializeField] private GameObject[] bossAreabounds = new GameObject[4];

    private int level = 0;

    // Start is called before the first frame update
    void Start()
    {
        /*
        level = GameManager.Instance.Level;
        GameObject.Instantiate(bosses[level], gameObject.transform.position, gameObject.transform.rotation);
        bossAreabounds[level].SetActive(true);
        */

        Dictionary<string, bool> giants = AchievementManager.Instance.SlayedGiants;

        /*
        
        foreach (KeyValuePair<string, bool> item in giants)
        {
            
            if (item.Value)
            {
                bosses[level].SetActive(true);
            }

            level += 1; // boss 0 = fire, 1 = earth.. osv
        }
        */

        bool firebossDead;
        giants.TryGetValue("Fire", out firebossDead);
        if(firebossDead){
            bosses[0].SetActive(false);
        }

        bool earthbossDead;
        giants.TryGetValue("Earth", out earthbossDead);
        if (earthbossDead)
        {
            bosses[1].SetActive(false);
        }

        bool waterbossDead;
        giants.TryGetValue("Water", out waterbossDead);
        if (waterbossDead)
        {
            bosses[2].SetActive(false);
        }

        bool windbossDead;
        giants.TryGetValue("Wind", out windbossDead);
        if (windbossDead)
        {
            bosses[3].SetActive(false);
        }

    }
    
}
