//Author: Niklas Almqvist
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliasManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<EliasPlayer>().StartElias();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
