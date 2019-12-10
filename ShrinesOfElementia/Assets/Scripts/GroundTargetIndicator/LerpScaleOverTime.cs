// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpScaleOverTime : MonoBehaviour
{
    [SerializeField] private GameObject objectToScale = null;

    [SerializeField] private Vector3 startScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 endScale = new Vector3(1f, 1f, 1f);

    [SerializeField] private float durationInSeconds = 1f;
    private float timeRemainingInSeconds;

    [SerializeField] private bool destroyObjectAfterLerp = true;



    // Fix this
    private void Awake()
    {
        timeRemainingInSeconds = durationInSeconds;
    }

    private void Update()
    {
        float deltaTime = timeRemainingInSeconds / durationInSeconds;

        objectToScale.transform.localScale = new Vector3(Mathf.SmoothStep(startScale.x, endScale.x, deltaTime), Mathf.SmoothStep(startScale.y, endScale.y, deltaTime), Mathf.SmoothStep(startScale.z, endScale.z, deltaTime));

        if(destroyObjectAfterLerp == true && timeRemainingInSeconds <= 0f)
        {
            Destroy(gameObject);
        }

        timeRemainingInSeconds -= Time.deltaTime;
    }
}
