using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public class Controls
{
    public String name;
    [HideInInspector] public String keyCodeString;
    [HideInInspector] public KeyCode keyCode;
    public String defaultKeycode;

    public String GetNicerName()
    {
        if(keyCodeString.Length >= 5 && keyCodeString.Substring(0, 5) == "Alpha")
        {
            //Debug.Log("keycode[4] = " + keyCodeString.Substring(5));
            return keyCodeString[5].ToString();
        }

        return keyCodeString;
    }
}

[Serializable]
public class SensitivityControl
{
    public int defaultSensitivity = 100;
    [HideInInspector] public int chosenSensitivity;
}

public class InputManager : MonoBehaviour
{

    public static InputManager Instance { get; private set; }

    public Controls[] controls;

    public Dictionary<string, Controls> keyCode { get; private set; }

    public SensitivityControl sensitivity;

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(gameObject); }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }

        keyCode = new Dictionary<string, Controls>();

        ReloadKeys();

        //Debug.LogWarning("COUNT: " + keyCode.Count());

    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ReloadKeys()
    {
        keyCode.Clear();
        foreach (Controls control in controls)
        {
            String newKey = PlayerPrefs.GetString("Key_" + control.name);
            if (newKey.Length > 0)
            {
                control.keyCodeString = newKey;
            }
            else
            {
                control.keyCodeString = control.defaultKeycode;
            }

            //Debug.Log("newKey: " + newKey + " --- Length: " + newKey.Length);
            control.keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), control.keyCodeString);
            keyCode.Add(control.name, control);
        }

        //Debug.Log("USEDEFAULTSENSITIVITY: " + PlayerPrefs.GetInt("UseDefaultSensitivity"));
        if(PlayerPrefs.GetInt("HasChangedSensitivity") == 0)
        {
            sensitivity.chosenSensitivity = sensitivity.defaultSensitivity;
        } else
        {
            sensitivity.chosenSensitivity = PlayerPrefs.GetInt("sensitivity");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("KEYCODE FOR FORWARD: " + keyCode["Water"].GetNicerName());
    }
}
