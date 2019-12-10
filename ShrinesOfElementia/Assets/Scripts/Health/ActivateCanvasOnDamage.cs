// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCanvasOnDamage : MonoBehaviour
{
    [SerializeField] private GameObject canvas = null;

    public bool CanvasIsActive { get; set; } = false;

    private void Awake()
    {
        if(canvas != null)
        {
            canvas.SetActive(CanvasIsActive);
        }
    }

    public void ActivateCanvas()
    {
        canvas.SetActive(true);
        CanvasIsActive = true;
    }
}
