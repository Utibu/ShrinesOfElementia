using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivesController : MonoBehaviour
{

    [SerializeField] private GameObject[] lives = new GameObject[3];


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameManager.Instance.PlayerLivesRemaining; i++)
        {
            lives[i].SetActive(true);
        }

        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
