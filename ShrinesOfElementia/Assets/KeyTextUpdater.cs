using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyTextUpdater : MonoBehaviour
{
    private TextMeshProUGUI textGUI;

    [SerializeField] string keyName;

    [SerializeField] bool useCustomText;
    [SerializeField] string textBefore;
    [SerializeField] string textAfter;
    // Start is called before the first frame update
    void Start()
    {
        textGUI = GetComponent<TextMeshProUGUI>();

        if(textGUI != null)
        {
            if(useCustomText)
            {
                textGUI.text = textBefore + "" + InputManager.Instance.keyCode[keyName].GetNicerName() + "" + textAfter;
            } else
            {
                textGUI.text = InputManager.Instance.keyCode[keyName].GetNicerName();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
