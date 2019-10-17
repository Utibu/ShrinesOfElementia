using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : Interactable
{
    [SerializeField] private ShrineElementActivator shrineElementActivator;

    private void Start()
    {

    }

    protected override void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            ShrineEvent shrineEvent = new ShrineEvent(shrineElementActivator);
        }
    }
}
