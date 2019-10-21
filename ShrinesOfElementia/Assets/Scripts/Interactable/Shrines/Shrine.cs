using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : Interactable
{
    private string element;
    [SerializeField] private Canvas interactCanvas;
    private ParticleSystem particleSystem;
    private enum SHRINETYPES
    {
        Fire,
        Water,
        Earth,
        Wind
    };

    [SerializeField] private SHRINETYPES shrineTypes;
    

    protected override void Start()
    {
        base.Start();
        particleSystem = GetComponent<ParticleSystem>();

        switch (shrineTypes)
        {
            case SHRINETYPES.Fire:
                element = "Fire";
                print("Fire active");
                break;
            case SHRINETYPES.Water:
                element = "Water";
                print("Water active");

                break;
            case SHRINETYPES.Earth:
                element = "Earth";
                print("Earth active");

                break;
            case SHRINETYPES.Wind:
                element = "Earth";
                print("Wind active");

                break;

        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        interactCanvas.gameObject.SetActive(true);
    }

    protected override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }

    protected override void OnInteract()
    {
        print("In shrine on interact");
        ShrineEvent shrineEvent = new ShrineEvent(element);
        shrineEvent.FireEvent();
        particleSystem.Stop();
        Disable();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Left");
        if(other.gameObject.tag == "Player")
        {
            interactCanvas.gameObject.SetActive(false);
        }
    }

    protected override void Disable()
    {
        print("in shrine disable");
        boxCollider.enabled = false;
        interactCanvas.gameObject.SetActive(false);
    }

}
