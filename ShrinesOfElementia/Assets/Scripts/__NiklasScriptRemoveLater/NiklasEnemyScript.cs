//Author: Niklas Almqvist
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiklasEnemyScript : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            enemy.GetComponent<Animator>().SetTrigger("ShouldAttack");
        }
    }
}
