using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    protected GameObject caster;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    protected void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
