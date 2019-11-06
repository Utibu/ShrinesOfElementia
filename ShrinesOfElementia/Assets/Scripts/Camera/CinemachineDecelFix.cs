using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineDecelFix : MonoBehaviour
{
    private CinemachineFreeLook freelook;

    [SerializeField] private float horizontalAimingSpeed;
    [SerializeField] private float verticalAimingSpeed;
    [SerializeField] private float yCorrection;
                     
    private float xAxisValue;
    private float yAxisValue;                 
    private float mouseX;
    private float mouseY;

    void Start()
    {
        freelook = GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        mouseX = Input.GetAxis("MouseX") * horizontalAimingSpeed * Time.deltaTime;
        mouseY = Input.GetAxis("MouseY") * verticalAimingSpeed * Time.deltaTime;

        mouseY /= 360f;
        mouseY *= yCorrection;

        xAxisValue += mouseX;
        yAxisValue = Mathf.Clamp01(yAxisValue - mouseY);

        freelook.m_XAxis.Value = xAxisValue;
        freelook.m_YAxis.Value = yAxisValue;
    }
}
