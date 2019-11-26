//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private GameObject owner;
    private float time = 100f;
    private float countdown = 100f; // if 0, the destroy in update will happen before SetVariables(). no bueno 
    private System.Action functionToCall;




    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown < 0)
        {
            if(owner != null)
            {
                Debug.Log("CALLING FROM TIMER");
                functionToCall();
            }
            Destroy(this.gameObject);
        }
    }

    //initialize timer duration and action. Wanted to do this with a constructor of course but seems to not go well with instantiate(). 
    public void SetVariables(GameObject owner, float duration, System.Action functionToCall)
    {
        this.owner = owner;
        this.time = duration;
        this.functionToCall = functionToCall;

        countdown = time;
    }
}
