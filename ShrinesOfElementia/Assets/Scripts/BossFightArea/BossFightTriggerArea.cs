// Author: Bilal El Medkouri

using UnityEngine;

public class BossFightTriggerArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("BossFightTriggered");
            BossEvents.Instance.BossFightAreaTriggerEnter();
        }
    }
}
