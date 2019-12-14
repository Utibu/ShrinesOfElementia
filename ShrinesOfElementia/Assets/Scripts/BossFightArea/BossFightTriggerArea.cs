// Author: Bilal El Medkouri

using UnityEngine;

public class BossFightTriggerArea : MonoBehaviour
{

    [SerializeField] private GameObject bossHealthBar;

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
            Giant.Instance.StartBattle();
            bossHealthBar.SetActive(true);
        }
    }


}
