using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineEventListener : MonoBehaviour
{
    private void Start()
    {
        EventSystem.Current.RegisterListener<ShrineEvent>(OnShrineEvent);
    }
    private void OnShrineEvent(ShrineEvent shrineEvent)
    {
        string element = shrineEvent.Element;
        print(shrineEvent.eventDescription);

        switch (element)
        {
            case "Fire":
                break;
            case "Water":
                shrineEvent.Player.GetComponent<HealthComponent>().CurrentHealth += 10;
                break;
            case "Earth":
                break;
            case "Wind":
                break;

        }
    }
}
