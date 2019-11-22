// Author: Niklas Almqvist
// Co-Author: Joakim Ljung

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

    [SerializeField] private Image fireIcon;
    [SerializeField] private Image waterIcon;
    [SerializeField] private Image windIcon;
    [SerializeField] private Image earthIcon;

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
