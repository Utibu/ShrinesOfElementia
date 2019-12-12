using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainExpOnU : MonoBehaviour
{
    [SerializeField] private float experience = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            EventManager.Instance.FireEvent(new ExperienceEvent("", experience));
        }
    }
}
