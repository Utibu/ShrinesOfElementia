using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilOldManController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.RegisterListener<BossDeathEvent>(OnBossDeath);
    }
    

    private void OnBossDeath(BossDeathEvent ev)
    {
        if (ev.ElementType.Equals("Wind")) // bc its last boss.
        {
            gameObject.SetActive(true);
        }
    }
}
