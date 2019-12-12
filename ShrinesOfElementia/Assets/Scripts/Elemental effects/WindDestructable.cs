using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindDestructable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.RegisterListener<WindAbilityEvent>(OnWindEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnWindEvent(WindAbilityEvent ev)
    {
        if (ev.Target.GetInstanceID().Equals(gameObject.GetInstanceID()))
        {
            Destroy(gameObject);
        }
    }


    private void OnDestroy()
    {
        try{
            EventManager.Instance.UnregisterListener<WindAbilityEvent>(OnWindEvent);
        }
        catch (System.NullReferenceException )
        {

        }
        
    }

}
