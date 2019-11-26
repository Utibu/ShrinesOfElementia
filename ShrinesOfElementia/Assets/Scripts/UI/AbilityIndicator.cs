using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityIndicator : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    [SerializeField] private float indicatorRange;
    [SerializeField] private LayerMask layerMask;


    private void Start()
    {

    }
    private void Update()
    {
        if (indicator != null)
        {
            //for testing, remove when not needed anymore
            if (Input.GetKey(KeyCode.T))
            {
                ShowIndicator();
            }
            else
            {
                HideIndicator();
            }
        }
    }

    private void ShowIndicator()
    {
        indicator.SetActive(true);
        RaycastHit hit;
        if (Physics.Raycast(CameraReference.Instance.transform.position, CameraReference.Instance.transform.forward, out hit, indicatorRange, layerMask))
        {
            Quaternion rayRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            if(rayRotation.eulerAngles.x < 30f || rayRotation.eulerAngles.x > -30f || rayRotation.eulerAngles.z < 30f || rayRotation.eulerAngles.z > -30f)
            {
                indicator.transform.position = new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z);
                indicator.transform.rotation = rayRotation;
                print(rayRotation.eulerAngles);
            }
            else
            {
                print("angle too steep");
            }
        }
    }

    private void HideIndicator()
    {
        indicator.SetActive(false);
    }
}
 