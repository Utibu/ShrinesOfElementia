//Author: Sofia Chyle Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthVinePassive : MonoBehaviour
{
    public bool IsReady { get; private set; }
    public bool IsActive { get; set; }
    private Dictionary<string, System.Action> abilities;


    [SerializeField] private GameObject giantPassivePrefab;
    [SerializeField] private float timeUntilActivation;
    


    private void DisableEarthVines()
    {
        IsActive = false;

    }

    private void ActivateEarthVines()
    {
        IsActive = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
