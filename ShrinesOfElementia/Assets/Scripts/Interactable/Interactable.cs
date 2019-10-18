using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected bool isInteractable;
    protected BoxCollider boxCollider;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    protected virtual void OnTriggerStay(Collider other)
    {

    }

    protected virtual void Disable()
    {
        
    }
}
