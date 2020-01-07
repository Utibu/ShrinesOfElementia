using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CHOSEN SENS: " + InputManager.Instance.sensitivity.chosenSensitivity);
        GetComponent<FreeLookAxisDriver>().xAxis.multiplier = Mathf.Clamp(InputManager.Instance.sensitivity.chosenSensitivity, 20, 100) / 50f;
        GetComponent<FreeLookAxisDriver>().yAxis.multiplier = 0.5f / - Mathf.Clamp(100 - InputManager.Instance.sensitivity.chosenSensitivity - 1, 20, 80);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
