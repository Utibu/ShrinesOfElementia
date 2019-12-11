using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{

    private static PlayerSpawn current;
    public static PlayerSpawn Current
    {
        get
        {
            if (current == null)
            {
                current = GameObject.FindObjectOfType<PlayerSpawn>();
            }
            return current;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

   
}
