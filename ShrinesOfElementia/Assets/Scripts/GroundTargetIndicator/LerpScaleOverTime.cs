// Author: Bilal El Medkouri

using UnityEngine;

public class LerpScaleOverTime : MonoBehaviour
{
    [SerializeField] private GameObject objectToScale = null;

    private Vector3 startScale;
    [SerializeField] private Vector3 endScale = new Vector3(1f, 1f, 1f);

    [SerializeField] private float durationInSeconds = 1f;
    private float startTime;

    [SerializeField] private bool destroyThisObjectAfterLerp = true;

    private void Awake()
    {
        startScale = objectToScale.transform.localScale;
        startTime = Time.time;
    }

    private void Update()
    {
        float interpolater = (Time.time - startTime) / durationInSeconds;
        if (Time.time - startTime < durationInSeconds)
        {
            objectToScale.transform.localScale = new Vector3(Mathf.Lerp(startScale.x, endScale.x, interpolater),
                Mathf.Lerp(startScale.y, endScale.y, interpolater),
                Mathf.Lerp(startScale.z, endScale.z, interpolater));
        }

        if (destroyThisObjectAfterLerp == true && Time.time - startTime >= durationInSeconds)
        {
            Destroy(gameObject);
        }

        print("Timer: " + (Time.time - startTime));
        print("Start scale: " + startScale);
        print("Current scale: " + objectToScale.transform.localScale);
        print("End scale: " + endScale);
    }
}
