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

    protected virtual void OnTriggerEnter(Collider other)
    {

    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }

    }

    protected virtual void OnInteract()
    {

    }

    protected virtual void Disable()
    {
        
    }
}
