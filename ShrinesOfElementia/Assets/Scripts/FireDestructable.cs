using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDestructable : MonoBehaviour
{
    public GameObject particles;

    void Start()
    {
        EventManager.Instance.RegisterListener<FireAbilityEvent>(OnFireEvent);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnFireEvent(FireAbilityEvent ev)
    {
        if (ev.Target.GetInstanceID().Equals(gameObject.GetInstanceID()))
        {
            particles.GetComponent<ParticleSystem>().Stop();
            TimerManager.Instance.SetNewTimer(gameObject, 1.5f, Destroy);
            gameObject.GetComponent<Collider>().enabled = false;

            
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        try
        {
            EventManager.Instance.UnregisterListener<FireAbilityEvent>(OnFireEvent);
        }
        catch (System.NullReferenceException)
        {

        }

    }
}
