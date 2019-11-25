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
        if (Input.GetKey(KeyCode.T))
        {
            ShowIndicator();
        }
    }

    private void ShowIndicator()
    {
        RaycastHit hit;
        if (Physics.Raycast(CameraReference.Instance.transform.position, CameraReference.Instance.transform.forward, out hit, indicatorRange, layerMask))
        {
            GameObject indicatorObject = Instantiate(indicator, hit.point, Quaternion.Euler(hit.point));
            indicatorObject.transform.position += new Vector3(0.0f, 0.1f, 0.0f);
        }
    }
}
