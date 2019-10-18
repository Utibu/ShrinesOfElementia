using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : Interactable
{
    private string element;
    [SerializeField] private Canvas interactCanvas;
    public enum ShrineTypes
    {
        Fire,
        Water,
        Earth,
        Wind
    };

    private void Start()
    {

    }

    protected override void OnTriggerStay(Collider other)
    {
        interactCanvas.gameObject.SetActive(true);
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            ShrineEvent shrineEvent = new ShrineEvent(element);
            shrineEvent.FireEvent();
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
}
