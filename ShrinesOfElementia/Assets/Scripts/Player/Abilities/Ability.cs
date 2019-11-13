using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public GameObject Caster { get { return caster; } set { caster = value; } }
    protected GameObject caster;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    protected virtual void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
