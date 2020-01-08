//Author: Niklas Almqvist
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//Author: https://www.studica.com/blog/custom-input-manager-unity-tutorial
//Co-author: Niklas Almqvist

[Serializable]
public class KeyOption
{
    public String keyName;
    public bool isDisabled = false;
    public TextMeshProUGUI label;
    public TextMeshProUGUI buttonLabel;
}

public class ControlsMenuManager : MonoBehaviour
{
    [SerializeField] private Transform menuPanel;
    [SerializeField] private List<String> currentKeybinds = new List<string>();
    Event keyEvent;
    TextMeshProUGUI buttonText;
    KeyCode newKey;
    bool waitingForKey;

    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityLabel;


    [SerializeField] private KeyOption[] keyOptions;
    [SerializeField] private List<String> keyStrings = new List<string>();
    private Dictionary<TextMeshProUGUI, KeyOption> keysDict = new Dictionary<TextMeshProUGUI, KeyOption>();



    void Start()
    {
        waitingForKey = false;

        //Uncomment to reset 
        //PlayerPrefs.SetInt("HasChangedKeys", 0);
        //PlayerPrefs.DeleteAll();

        //Debug.Log("HASCHANGEDFKEYS: " + PlayerPrefs.GetInt("HasSetupKeys"));

        /*foreach(KeyOption option in keyOptions)
        {
            //option.keyName = option.label.text;

            if (PlayerPrefs.GetInt("HasSetupKeys") == 0)
            {
                Debug.Log("ISRUNNINGBAD");
                PlayerPrefs.SetString("Key_" + option.keyName, option.buttonLabel.text);

                if (option.isDisabled == false)
                {
                    keyStrings.Add("Key_" + option.keyName);
                }

            } else
            {
                Debug.Log(PlayerPrefs.GetString("Key_" + option.keyName));
                option.buttonLabel.text = PlayerPrefs.GetString("Key_" + option.keyName);
            }

            Debug.Log(PlayerPrefs.GetString("Key_" + option.keyName));
            currentKeybinds.Add(option.buttonLabel.text);
            keysDict.Add(option.buttonLabel, option);

            

        }
        if (PlayerPrefs.GetInt("HasSetupKeys") == 0)
        {
            Debug.Log("NOWCHANGEDKEYS: " + PlayerPrefs.GetInt("HasSetupKeys"));
            PlayerPrefs.SetInt("HasSetupKeys", 1);
            PlayerPrefs.SetString("KeyNames", String.Join(",", keyStrings));
        }*/

        //Debug.Log("LENGTH: " + keyOptions.Length);
        foreach (KeyOption option in keyOptions)
        {
            //Debug.Log("--- " + option.keyName);
            //Debug.Log("InputManager.Instance.keyCode[option.keyName].keyCodeString: " + InputManager.Instance.keyCode[option.keyName].keyCodeString);
            option.buttonLabel.text = InputManager.Instance.keyCode[option.keyName].keyCodeString;

            currentKeybinds.Add(option.buttonLabel.text);
            keysDict.Add(option.buttonLabel, option);
        }

        sensitivitySlider.value = InputManager.Instance.sensitivity.chosenSensitivity;
    }





    void Update()

    {

        //Escape key will open or close the panel

       /* if (Input.GetKeyDown(KeyCode.Escape) && !menuPanel.gameObject.activeSelf)

            menuPanel.gameObject.SetActive(true);

        else if (Input.GetKeyDown(KeyCode.Escape) && menuPanel.gameObject.activeSelf)

            menuPanel.gameObject.SetActive(false);
            */
    }



    void OnGUI()

    {

        /*keyEvent dictates what key our user presses

         * bt using Event.current to detect the current

         * event

         */

        keyEvent = Event.current;



        //Executes if a button gets pressed and

        //the user presses a key

        if (keyEvent.isKey && waitingForKey)

        {

            newKey = keyEvent.keyCode; //Assigns newKey to the key user presses

            waitingForKey = false;

        }

    }



    /*Buttons cannot call on Coroutines via OnClick().

     * Instead, we have it call StartAssignment, which will

     * call a coroutine in this script instead, only if we

     * are not already waiting for a key to be pressed.

     */

    public void StartAssignment(TextMeshProUGUI text)
    {
        string keyName = keysDict[text].keyName;
        Debug.Log("KEYNAME: " + keyName);
        if (!waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }



    //Assigns buttonText to the text component of
    //the button that was pressed
    public void SendText(TextMeshProUGUI text)
    {
        buttonText = text;
    }

    //Used for controlling the flow of our below Coroutine

    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
            yield return null;
    }

    public IEnumerator AssignKey(string keyName)

    {

        waitingForKey = true;
        yield return WaitForKey(); //Executes endlessly until user presses a key

        if(!currentKeybinds.Contains(newKey.ToString()))
        {
            buttonText.text = newKey.ToString();
            PlayerPrefs.SetString("Key_" + keysDict[buttonText].keyName, newKey.ToString());
            //Debug.Log("NEWKEY: " + keysDict[buttonText].keyName + "  - - -- - - " + newKey.ToString());
            PlayerPrefs.Save();
            InputManager.Instance.ReloadKeys();

            currentKeybinds.Clear();
            foreach (KeyOption option in keyOptions)
            {
                currentKeybinds.Add(option.buttonLabel.text);

               
            }
        }
       

        yield return null;

    }

    public void OnSensitivitySliderChange(float val)
    {
        InputManager.Instance.sensitivity.chosenSensitivity = (int)val;
        PlayerPrefs.SetInt("sensitivity", (int)val);
        Debug.Log("SENSITIVITY: " + PlayerPrefs.GetInt("sensitivity"));
        PlayerPrefs.SetInt("HasChangedSensitivity", 1);
        sensitivityLabel.text = "" + val;
        InputManager.Instance.ReloadKeys();
    }
}
