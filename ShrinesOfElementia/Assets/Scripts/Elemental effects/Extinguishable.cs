//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguishable : MonoBehaviour
{

    [SerializeField]private float ReviveTime;
    private float timer;
    private bool extinguished;

    private ParticleSystem fire;

    // Start is called before the first frame update
    void Start()
    {
        extinguished = false;
        fire = GetComponent<ParticleSystem>();
        EventSystem.Current.RegisterListener<GeyserCastEvent>(Extinguish);

    }

    // Update is called once per frame
    void Update()
    {
        if (extinguished)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                extinguished = false;
                timer = ReviveTime;
                fire.Play();
            }
        }
    }

    private void Extinguish(GeyserCastEvent ev)
    {
        /*
        Debug.Log("FIRE IS EXTINGUISHHDD");
        timer = ReviveTime;
        extinguished = true;
        fire.Stop();
        */

        if (Vector3.Distance(this.transform.position, ev.affectedPosition) <= ev.effectRange)
        {
            Debug.Log("FIRE IS EXTINGUISHHDD");
            timer = ReviveTime;
            extinguished = true;
            fire.Stop();
        }
        else
        {
            Debug.Log("geyser not close enough to fire");
        }
    }

}
