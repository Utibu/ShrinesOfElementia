//Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : DroppableObject
{

    [SerializeField] private float vacuumSpeed;
    [SerializeField] private GameObject vacuumPoint;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))// && Player.Instance.Health.CurrentHealth != Player.Instance.Health.MaxHealth)
        {
            droppedObject.transform.position = Vector3.MoveTowards(droppedObject.transform.position, other.transform.position + Vector3.up * 1.5f, vacuumSpeed * Time.deltaTime);
        }
    }
    //droppedObject.transform.position = Vector3.MoveTowards(droppedObject.transform.position, vacuumPoint.transform.position, vacuumSpeed * Time.deltaTime);
}
