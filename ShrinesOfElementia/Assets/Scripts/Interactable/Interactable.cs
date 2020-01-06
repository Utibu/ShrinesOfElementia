//Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected bool isInteractable;
    protected BoxCollider boxCollider;
    [SerializeField] protected int experience;
    private bool expTaken = false;
    [SerializeField] protected Canvas interactCanvas;


    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactCanvas.gameObject.SetActive(true);

            //if palyer triggers point of interest canvas, give XP once.
            if (gameObject.CompareTag("POI"))
            {
                OnInteract();
            }
        }
        
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
        

    }

    protected void OnTriggerExit(Collider other)
    {
        Debug.Log("Left");
        if (other.gameObject.tag == "Player")
        {
            interactCanvas.gameObject.SetActive(false);
        }
    }

    protected virtual void OnInteract()
    {
        
        print("interacted");
        if (!expTaken)
        {
            EventManager.Instance.FireEvent(new ExperienceEvent(experience + " gained", experience));
            expTaken = true;
        }
        
    }

    protected virtual void Disable()
    {
        
    }
}
