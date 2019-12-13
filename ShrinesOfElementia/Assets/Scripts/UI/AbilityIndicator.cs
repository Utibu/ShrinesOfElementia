//Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityIndicator : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    [SerializeField] private float indicatorRange;
    [SerializeField] private float indicatorDistanceFromGround;
    [SerializeField] private LayerMask layerMask;

    public Vector3 GetIndicatorPoint()
    {
        return indicator.transform.position;
    }

    public void ShowIndicator()
    {
        indicator.SetActive(true);
        RaycastHit hit;
        if (Physics.Raycast(CameraReference.Instance.transform.position, CameraReference.Instance.transform.forward, out hit, indicatorRange, layerMask))
        {
            
            Quaternion rayRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            if(rayRotation.eulerAngles.x < 30f || rayRotation.eulerAngles.x > -30f || rayRotation.eulerAngles.z < 30f || rayRotation.eulerAngles.z > -30f) //not working
            {
                print("showing indicator");
                indicator.transform.position = new Vector3(hit.point.x, hit.point.y + indicatorDistanceFromGround, hit.point.z);
                indicator.transform.rotation = rayRotation;
            }
            else
            {
                //not working, should be implemented
                print("angle too steep");
            }
        }
    }

    public void HideIndicator()
    {
        print("hiding indicator");
        indicator.SetActive(false);
    }
}
 