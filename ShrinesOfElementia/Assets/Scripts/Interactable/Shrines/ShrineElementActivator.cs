using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineElementActivator : MonoBehaviour
{
    private void Start()
    {
        EventSystem.Current.RegisterListener<ShrineEvent>(OnShrineEvent);
    }

    private void OnShrineEvent(ShrineEvent shrineEvent)
    {
        print(shrineEvent.Element + " shrine activated");
        switch (shrineEvent.Element)
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
