//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBurnDamage : MonoBehaviour
{
    [SerializeField] private int DamagePerTick;
    private float tickTime = 0.5f;
    private float countdown;
    private HealthComponent burnvictim;
    private bool isActive = true;

    public bool IsActive { get => isActive; set => isActive = value; }

    // Start is called before the first frame update
    void Start()
    {
        countdown = tickTime;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<HealthComponent>(out burnvictim) && isActive)
        {
            countdown -= Time.deltaTime;
            if (countdown < 0)
            {
                burnvictim.CurrentHealth -= DamagePerTick;
                Debug.Log("burndamage dealt");
                countdown = tickTime;
            }
        }
    }

    
    
    
}
