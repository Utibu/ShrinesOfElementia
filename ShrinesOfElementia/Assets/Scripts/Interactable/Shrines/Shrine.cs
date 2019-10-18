using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : Interactable
{
    private string element;
    [SerializeField] private Canvas interactCanvas;
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

    protected override void OnTriggerStay(Collider other)
    {
        interactCanvas.gameObject.SetActive(true);
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            ShrineEvent shrineEvent = new ShrineEvent(element);
            shrineEvent.FireEvent();
            Disable();
            Debug.Log("Shrine event fired");
        }
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
        boxCollider.enabled = false;
        interactCanvas.gameObject.SetActive(false);
    }

}
