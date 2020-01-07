//Author: Joakim Ljung
//Co-author: Niklas Almqvist, Sofia Kauko

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SHRINETYPES
{
    Fire,
    Water,
    Earth,
    Wind,
    Great
};

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
        EventManager.Instance.RegisterListener<StaggerEvent>(OnStagger);
        EventManager.Instance.RegisterListener<PlayerDeathEvent>(OnPlayerDeath);
    }


    protected override void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E) && Player.Instance.CharacterController.velocity.magnitude <= 0)
        {
            if (channelTimer == null)
            {
                Player.Instance.Animator.SetTrigger("OnPray"); // Add some channel animation
                //Player.Instance.Animator.SetBool("InCombat", false); // stops player from rotating to face camera.forward
                channelTimer = TimerManager.Instance.SetNewTimer(gameObject, channelTime, OnInteract);
            }
        }
        else if (other.gameObject.tag == "Player" && Input.anyKeyDown && channelTimer != null)
        {
            print("cancelled channel");
            Player.Instance.Animator.SetTrigger("ToNeutral");
            Player.Instance.GetComponent<ParticleManager>().HideShrineActivationParticles();
            Destroy(channelTimer);
            channelTimer = null;
        }
    }
    
    //protected void OnTriggerExit(Collider other)
    //{
        /*Player.Instance.GetComponent<ParticleManager>().HideShrineActivationParticles();
        Debug.Log(Player.Instance.GetComponent<ParticleManager>() + " WEEEE");*/
    //}
    
    protected override void OnInteract()
    {
        base.OnInteract();
        Disable();
        Player.Instance.Animator.SetTrigger("ToNeutral");
        Player.Instance.GetComponent<ParticleManager>().HideShrineActivationParticles();
        ShrineEvent shrineEvent = new ShrineEvent(element + " shrine activated", element);
        EventManager.Instance.FireEvent(shrineEvent);
        shrineAnimationController.SetTrigger("IsTaken");
        Player.Instance.GetComponent<PlayerSoundController>().PlayShrineTakenClip();
        //Player.Instance.GetComponent<ShrineUnlockComponent>().ShowUnlockableCanvas(firstUnlockableText, secondUnlockableText);
        Player.Instance.GetComponent<ShrineUnlockComponent>().OpenCanvas(shrineTypes);
        //shrinePanel.gameObject.SetActive(true);
        //Player.Instance.GetComponent<MovementInput>().TakeInput = false;
        beacon.SetActive(false);
    }

    private void OnStagger(StaggerEvent eve)
    {
        Destroy(channelTimer);
        Player.Instance.GetComponent<ParticleManager>().HideShrineActivationParticles();
    }

    private void OnPlayerDeath(PlayerDeathEvent eve)
    {
        interactCanvas.gameObject.SetActive(false);
    }

    protected override void Disable()
    {
        boxCollider.enabled = false;
        interactCanvas.gameObject.SetActive(false);
    }

}
