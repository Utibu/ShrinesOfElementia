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

    private void ShowIndicator()
    {
        indicator.SetActive(true);
        RaycastHit hit;
        if (Physics.Raycast(CameraReference.Instance.transform.position, CameraReference.Instance.transform.forward, out hit, indicatorRange, layerMask))
        {
            print(Quaternion.Euler(hit.normal));
            indicator.transform.position = new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z);
            indicator.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
    }

    private void HideIndicator()
    {
        indicator.SetActive(false);
    }
}
 