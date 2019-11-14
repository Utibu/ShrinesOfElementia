//Author: Niklas Almqvist

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderFog : MonoBehaviour
{
    [SerializeField] private Transform player;
    private bool isActive = false;
    [SerializeField] private float distanceToShow;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private ParticleSystem.MinMaxGradient nearColor;
    [SerializeField] private ParticleSystem.MinMaxGradient farColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {//27, 29
        if(!isActive && Vector3.Distance(this.transform.position, player.transform.position) >= distanceToShow)
        {
            isActive = true;
            var ps = particles.main;
            //ps.startColor = new ParticleSystem.MinMaxGradient(new Color(1f, 1f, 1f, 0.8f));
            ps.startColor = farColor;
            particles.Play();
        } else if (isActive && Vector3.Distance(this.transform.position, player.transform.position) < distanceToShow)
        {
            isActive = false;
            var ps = particles.main;
            //ps.startColor = new ParticleSystem.MinMaxGradient(new Color(1f, 1f, 1f, 0.8f));
            ps.startColor = nearColor;
            particles.Play();
        }
    }
}
