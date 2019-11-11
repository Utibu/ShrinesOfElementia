using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineElementActivator : MonoBehaviour
{
    private void Start()
    {
        EventManager.Current.RegisterListener<ShrineEvent>(OnShrineEvent);
    }

    private void OnShrineEvent(ShrineEvent shrineEvent)
    {
        print(shrineEvent.Element + " shrine activated");
        switch (shrineEvent.Element)
        {
            case "Fire":
                break;
            case "Water":
                shrineEvent.Player.GetComponent<HealthComponent>().CurrentHealth = shrineEvent.Player.GetComponent<HealthComponent>().MaxHealth;
                break;
            case "Earth":
                break;
            case "Wind":
                break;
        }
    }
}