//Author: Niklas Almqvist

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private GameObject shrineActivationParticles;

    public void ShowShrineActivationParticles()
    {
        shrineActivationParticles.SetActive(true);
        //shrineActivationParticles.GetComponent<ParticleSystem>().Play();
    }

    public void HideShrineActivationParticles()
    {
        shrineActivationParticles.SetActive(false);
        /*
        shrineActivationParticles.GetComponent<ParticleSystem>().Stop();
        Invoke("StopShrinesActivationParticles", 5);*/
    }

    private void StopShrinesActivationParticles()
    {
        //shrineActivationParticles.SetActive(false);
    }
}
