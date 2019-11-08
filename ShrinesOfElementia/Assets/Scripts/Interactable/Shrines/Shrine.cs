﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shrine : Interactable
{
    private string element;
    [SerializeField] private Canvas interactCanvas;
    [SerializeField] private Animator shrineAnimationController;
    [SerializeField] private GameObject beacon;
    [SerializeField] private RectTransform shrinePanel;
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
                element = "Wind";
                break;

        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactCanvas.gameObject.SetActive(true);
        }
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
        //shrinePanel.gameObject.SetActive(true);
        //Player.Instance.GetComponent<MovementInput>().TakeInput = false;
        beacon.SetActive(false);
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
