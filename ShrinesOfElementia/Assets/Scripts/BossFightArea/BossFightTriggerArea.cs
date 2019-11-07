// Author: Bilal El Medkouri

using UnityEngine;

public class BossFightTriggerArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BossEvents.Instance.BossFightAreaTriggerEnter();
    }
}
