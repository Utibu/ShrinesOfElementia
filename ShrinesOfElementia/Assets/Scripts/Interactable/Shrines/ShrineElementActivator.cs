//Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShrineElementActivator : MonoBehaviour
{
    [SerializeField] private Image fireUnobtainedImage;
    [SerializeField] private Image waterUnobtainedImage;
    [SerializeField] private Image windUnobtainedImage;
    [SerializeField] private Image earthUnobtainedImage;

    private void Start()
    {
        EventManager.Instance.RegisterListener<ShrineEvent>(OnShrineEvent);
    }

    private void OnShrineEvent(ShrineEvent shrineEvent)
    {
        print(shrineEvent.Element + " shrine activated");
        switch (shrineEvent.Element)
        {
            case "Fire":
                if(fireUnobtainedImage != null)
                    fireUnobtainedImage.enabled = false;
                break;
            case "Water":
                if (waterUnobtainedImage != null)
                    waterUnobtainedImage.enabled = false;
                break;
            case "Wind":
                if (windUnobtainedImage != null)
                    windUnobtainedImage.enabled = false;
                break;
            case "Earth":
                if (earthUnobtainedImage != null)
                    earthUnobtainedImage.enabled = false;
                break;
        }
    }
}