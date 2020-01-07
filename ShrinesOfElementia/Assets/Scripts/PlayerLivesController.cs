//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivesController : MonoBehaviour
{

    [SerializeField] private GameObject[] lives = new GameObject[3];
    private int remainingLives;


    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.RegisterListener<PlayerDeathEvent>(RemoveLifeImage);
        
        for (int i = 0; i < GameManager.Instance.PlayerLivesRemaining; i++)
        {
            lives[i].SetActive(true);
            remainingLives = i; 
        }

        
    }

    private void RemoveLifeImage(PlayerDeathEvent ev)
    {
        if(remainingLives >= 0) // just in case it tries to do this before deathscreen loads.
        {
            lives[remainingLives].SetActive(false);
            remainingLives -= 1;
        }
        
    }
    
}
