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
    [SerializeField] private float destructionDelayInSecondsAfterLerp = 0f;
    private float destructionDelayTimer;
    private void Awake()
    {
        startScale = objectToScale.transform.localScale;
        startTime = Time.time;

        destructionDelayTimer = destructionDelayInSecondsAfterLerp;
    }

    private void Update()
    {
        float interpolater = (Time.time - startTime) / durationInSeconds;

        if (Vector3.Distance(startScale, endScale) > 0f)
        {
            objectToScale.transform.localScale = new Vector3(Mathf.Lerp(startScale.x, endScale.x, interpolater),
                Mathf.Lerp(startScale.y, endScale.y, interpolater),
                Mathf.Lerp(startScale.z, endScale.z, interpolater));
        }

        if (destroyThisObjectAfterLerp == true && Time.time - startTime >= durationInSeconds)
        {
            if (destructionDelayTimer <= 0f)
            {
                Destroy(gameObject);
            }
            destructionDelayTimer -= Time.deltaTime;
        }
    }
}
