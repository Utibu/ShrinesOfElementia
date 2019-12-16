using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InputManager : MonoBehaviour
{

    public static InputManager Instance { get; private set; }
    public Dictionary<string, KeyCode> keyCode { get; private set; }

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }

        keyCode = new Dictionary<string, KeyCode>();

        String k = PlayerPrefs.GetString("KeyNames");
        
        List<String> keys = k.Split(',').ToList();

        Debug.Log("KEYS: " + keys.Count);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
