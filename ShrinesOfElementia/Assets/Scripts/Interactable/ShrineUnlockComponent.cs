// Author: Niklas Almqvist

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShrineUnlockComponent : MonoBehaviour
{
    [SerializeField] private GameObject unlockableCanvas;
    [SerializeField] private Text firstUnlockText;
    [SerializeField] private Text secondUnlockText;
    [SerializeField] private float timeOpen;


    [SerializeField] private GameObject fireCanvas;
    [SerializeField] private GameObject windCanvas;
    [SerializeField] private GameObject earthCanvas;
    [SerializeField] private GameObject waterCanvas;

    public void ShowUnlockableCanvas(string firstText, string secondText)
    {
        /*firstUnlockText.text = firstText;
        secondUnlockText.text = secondText;
        unlockableCanvas.SetActive(true);
        Invoke("HideCanvas", timeOpen);*/
    }

    public void OpenCanvas(SHRINETYPES type)
    {
        switch(type)
        {
            case SHRINETYPES.Earth:
                earthCanvas.SetActive(true);
                break;
            case SHRINETYPES.Fire:
                fireCanvas.SetActive(true);
                break;
            case SHRINETYPES.Water:
                waterCanvas.SetActive(true);
                break;
            case SHRINETYPES.Wind:
                windCanvas.SetActive(true);
                break;
        }

        Invoke("HideCanvas", timeOpen);
    }

    private void HideCanvas()
    {
        unlockableCanvas.SetActive(false);

        windCanvas.SetActive(false);
        fireCanvas.SetActive(false);
        earthCanvas.SetActive(false);
        waterCanvas.SetActive(false);
    }
}
