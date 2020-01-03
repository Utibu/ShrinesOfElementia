using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Script by Yukun. The Timer Variant uses preplaced events and is specially designed to run unhampered inside Updates();
public class TimerVariant : MonoBehaviour
{
    public string tag;
    public float Interval = 2;
    public float IntervalTemp;
    public bool finished = true; //set to false to begin!
    public UnityEngine.Events.UnityEvent InvokeEnter;


    private void Start()
    {
        IntervalTemp = Interval;
    }

    private void Update()
    {
        if (!finished)
        {
            Interval -= Time.unscaledDeltaTime;
            if (Interval <= 0)
            {
                finished = true;
                Interval = IntervalTemp; //resets
                InvokeEnter.Invoke();

            }
        }
    }

    public void setStart(bool condition)
    {
        finished = !condition;
    }
}
