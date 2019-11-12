﻿//Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : DroppableObject
{

    [SerializeField] private float vacuumSpeed;
    [SerializeField] private GameObject vacuumPoint;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print(droppedObject.transform.position);
            droppedObject.transform.position = Vector3.MoveTowards(droppedObject.transform.position, vacuumPoint.transform.position, vacuumSpeed * Time.deltaTime);
        }
    }
}
