using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDestructable : MonoBehaviour
{
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
            GetComponent<ParticleSystem>().Stop();
            TimerManager.Instance.SetNewTimer(gameObject, 3f, Destroy);
            
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
