using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private State[] states; //The different states(classes) a gameobject can enter.
    private Dictionary<Type, State> allStates = new Dictionary<Type, State>(); // Dictionary of the instantiated states
    [SerializeField] private State currentState;
    [HideInInspector] public State lastState;

    //Give each object an own instance of the states in list -> states. Set and enter
    protected virtual void Awake()
    {
        foreach (State s in states)
        {
            State state = Instantiate(s);
            state.Initialize(this);
            allStates.Add(state.GetType(), state);
            if (currentState == null)
            {
                currentState = state;
            }
        }
        currentState.Enter();
    }


    public virtual void Start()
    {

    }
    
    //Called from statemachine that is a subclass to this statemachine. Makes sure the current states Update() is called. 
    public virtual void Update()
    {
        if (currentState != null)
        {
            currentState.HandleUpdate();
        }
    }


    public void Transition<T>() where T : State
    {
        currentState.Leave();
        lastState = currentState;
        currentState = allStates[typeof(T)];
        currentState.Enter();
    }


    public State GetCurrentState()
    {
        return currentState;
    }
}