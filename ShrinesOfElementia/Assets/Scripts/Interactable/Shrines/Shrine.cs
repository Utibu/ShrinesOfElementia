//Author: Joakim Ljung
//Co-author: Niklas Almqvist

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shrine : Interactable
{
    private string element;
    [SerializeField] private Animator shrineAnimationController;
    [SerializeField] private GameObject beacon;
    [SerializeField] private string firstUnlockableText;
    [SerializeField] private string secondUnlockableText;

    [SerializeField] private float channelTime;
    GameObject channelTimer;
    //[SerializeField] private RectTransform shrinePanel;
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



    protected override void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            if(channelTimer == null)
            {
                Player.Instance.GetComponent<Animator>().SetTrigger(""); // Add some channel animation
                channelTimer = TimerManager.Current.SetNewTimer(gameObject, channelTime, OnInteract);
            }
        }

        else if (other.gameObject.tag == "Player" && Input.anyKeyDown && channelTimer != null)
        {
            Destroy(channelTimer);
            channelTimer = null;
        }        
        
    }

    protected override void OnInteract()
    {
        base.OnInteract();
        Disable();
        ShrineEvent shrineEvent = new ShrineEvent(element + " shrine activated", element);
        EventManager.Current.FireEvent(shrineEvent);
        shrineAnimationController.SetTrigger("IsTaken");
        Player.Instance.GetComponent<ShrineUnlockComponent>().ShowUnlockableCanvas(firstUnlockableText, secondUnlockableText);
        //shrinePanel.gameObject.SetActive(true);
        //Player.Instance.GetComponent<MovementInput>().TakeInput = false;
        beacon.SetActive(false);
    }



    protected override void Disable()
    {
        boxCollider.enabled = false;
        interactCanvas.gameObject.SetActive(false);
    }

}
