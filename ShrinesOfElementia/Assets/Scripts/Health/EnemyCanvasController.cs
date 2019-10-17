// Author: Bilal El Medkouri

using UnityEngine;

public class EnemyCanvasController : MonoBehaviour
{
    private Vector3 targetPosition;

    private void Update()
    {
        targetPosition = new Vector3(CameraReference.Instance.transform.position.x, transform.position.y, CameraReference.Instance.transform.position.z);
        transform.LookAt(targetPosition);
    }
}
