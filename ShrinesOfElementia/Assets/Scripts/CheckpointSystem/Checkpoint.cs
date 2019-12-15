//Author jocke?
//co-Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]private GameObject spawnLocation;
    private Collider coll;
    private bool isActive;

    private void Start()
    {
        coll = GetComponent<Collider>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Checkpoint taken");
            isActive = true;
            EventManager.Instance.FireEvent(new CheckpointEvent("checkpoint activated", gameObject.transform.position));
            coll.enabled = false; // just turn off collider so it wont check on trigger all the time. 
        }
    }
}
