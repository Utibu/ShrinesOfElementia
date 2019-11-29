//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] public GameObject timerObject;
    private static TimerManager __Current;
    public static TimerManager Current
    {
        get
        {
            if (__Current == null)
            {
                __Current = GameObject.FindObjectOfType<TimerManager>();
            }
            return __Current;
        }
    }


    public GameObject SetNewTimer(GameObject owner, float duration, System.Action action)
    {
        //initialize timer prefab and set its variables. 
        GameObject timer = Instantiate(timerObject, gameObject.transform);
        timer.GetComponent<Timer>().SetVariables(owner, duration, action);
        return timer; 
    }

   
}
