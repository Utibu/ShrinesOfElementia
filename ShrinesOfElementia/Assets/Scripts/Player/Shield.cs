using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private BoxCollider shieldCollider;
    [SerializeField] private float staminaOnBlock;

    private void Start()
    {
        shieldCollider = GetComponent<BoxCollider>();
        shieldCollider.enabled = false;
    }
}
