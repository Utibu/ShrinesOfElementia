using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantWindAura : MonoBehaviour
{
    
    [SerializeField] private int DamagePerTick;
    private float tickTime = 0.5f;
    private float countdown;
    public GameObject PowerOnParticles;

    // Start is called before the first frame update
    void Start()
    {
        countdown = tickTime;
        PowerOnParticles.GetComponent<ParticleSystem>().Play();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            countdown -= Time.deltaTime;
            if (countdown < 0)
            {
                EventManager.Instance.FireEvent(new DamageEvent("windaura deals damage", DamagePerTick, gameObject, other.gameObject));
                Debug.Log("winddamage dealt");
                countdown = tickTime;
            }
        }
    }

    public void DestroySelf()
    {
        GetComponent<Collider>().enabled = false; // stop dealing damage
        GetComponentInChildren<ParticleSystem>().Stop();
        TimerManager.Instance.SetNewTimer(gameObject, 1.4f, Destroy);

    }

    private void Destroy()
    {
        Destroy(gameObject);
    }


}
