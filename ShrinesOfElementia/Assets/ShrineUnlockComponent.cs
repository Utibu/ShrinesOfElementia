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

    public void ShowUnlockableCanvas(string firstText, string secondText)
    {
        firstUnlockText.text = firstText;
        secondUnlockText.text = secondText;
        unlockableCanvas.SetActive(true);
        Invoke("HideCanvas", timeOpen);
    }

    private void HideCanvas()
    {
        unlockableCanvas.SetActive(false);
    }
}
