using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour
{

    [SerializeField] private RawImage[] abilityImages;
    private Text imageText;

    private void Start()
    {
        EventSystem.Current.RegisterListener<ShrineEvent>(OnShrineEvent);
    }

    private void OnShrineEvent(ShrineEvent eve)
    {
        foreach(RawImage image in abilityImages)
        {
            if(image.gameObject.name == eve.Element)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 255);
                imageText = image.GetComponentInChildren<Text>();
                imageText.color = new Color(imageText.color.r, imageText.color.g, imageText.color.b, 255f);
                return;
            }
        }
    }
}
