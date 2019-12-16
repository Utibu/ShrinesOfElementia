//Author: Sofia Kauko
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] public GameObject timerObject;

    public static TimerManager Instance { get; private set; }

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }
    }


    public GameObject SetNewTimer(GameObject owner, float duration, System.Action action)
    {
        //initialize timer prefab and set its variables. 
        GameObject timer = Instantiate(timerObject, gameObject.transform);
        timer.GetComponent<Timer>().SetVariables(owner, duration, action);
        return timer;
    }
}
