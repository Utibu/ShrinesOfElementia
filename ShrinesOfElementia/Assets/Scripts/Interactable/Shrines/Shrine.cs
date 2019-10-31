using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : Interactable
{
    private string element;
    [SerializeField] private Canvas interactCanvas;
    [SerializeField] private Animator shrineAnimationController;
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
                break;
            case SHRINETYPES.Water:
                element = "Water";
                break;
            case SHRINETYPES.Earth:
                element = "Earth";
                break;
            case SHRINETYPES.Wind:
                element = "Earth";
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
        ShrineEvent shrineEvent = new ShrineEvent(element + " shrine activated", element);
        EventSystem.Current.FireEvent(shrineEvent);
        shrineAnimationController.SetTrigger("IsTaken");
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
        boxCollider.enabled = false;
        interactCanvas.gameObject.SetActive(false);
    }

}
