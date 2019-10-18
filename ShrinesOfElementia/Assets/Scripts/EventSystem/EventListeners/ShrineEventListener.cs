using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineEventListener : EventListener<ShrineEvent>
{
    protected override void OnEvent(ShrineEvent shrineEvent)
    {
        string element = shrineEvent.Element;

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
