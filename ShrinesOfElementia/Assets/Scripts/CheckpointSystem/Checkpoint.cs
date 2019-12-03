using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //[SerializeField] private GameObject spawnLocation; wat?
    private bool isActive;

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Checkpoint taken");
            isActive = true;
            EventManager.Current.FireEvent(new CheckpointEvent("checkpoint activated", gameObject.transform.position));
        }
    }
}
