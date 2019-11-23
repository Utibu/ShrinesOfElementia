//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float time;
    private float countdown = 0f;
    private System.Action functionToCall;

    public Timer(float duration, System.Action functionToCall)
    {
        this.time = duration;
        this.functionToCall = functionToCall;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown < 0)
        {
            functionToCall();
            Destroy(this.gameObject);
        }
    }
}
