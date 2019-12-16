using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

// Author: Niklas Almqvist

[Serializable]
public class JournalEntry
{
    public Button Button;
    public JournalContent Content;
    [HideInInspector] public int id;
}

public class JournalManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject journalCanvas;
    public GameObject nextButton;
    public GameObject prevButton;
    [SerializeField] private JournalEntry[] entries;
    public GameObject cannotReadText;

    private Dictionary<Button, JournalEntry> entryDictionary = new Dictionary<Button, JournalEntry>();

    private Button currentlyOpen;
    
    public bool IsJournalOpen = false;

    void Start()
    {

        for(int i = 0; i < entries.Length; i++)
        {
            JournalEntry tempEntry = entries[i];
            tempEntry.id = i;
            entryDictionary.Add(entries[i].Button, tempEntry);
        }

        //OpenJournal();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("IsJournalOpen: " + IsJournalOpen);
        if(Input.GetKeyDown(KeyCode.J) && IsJournalOpen == false)
        {
            OpenJournal();
        } else if((Input.GetKeyDown(KeyCode.J) && IsJournalOpen == true) || Input.GetKeyDown(KeyCode.Escape))
        {
            CloseJournal();
        }
    }

    public void Next()
    {
        entryDictionary[currentlyOpen].Content.NextPage();
    }

    public void Prev()
    {
        entryDictionary[currentlyOpen].Content.PreviousPage();
    }

    public void OpenPage(Button button)
    {
        if(currentlyOpen != null)
        {
            entryDictionary[currentlyOpen].Content.gameObject.SetActive(false);
        }

        entryDictionary[button].Content.gameObject.SetActive(true);
        entryDictionary[button].Content.OnShowing();
        currentlyOpen = button;
    }

    public void OpenJournal()
    {
        journalCanvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        IsJournalOpen = true;
        OpenPage(entries[0].Button);
    }

    public void CloseJournal()
    {
        journalCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        IsJournalOpen = false;
    }

    public void OnCategoryClick()
    {
        Debug.LogWarning("CAT CLICK!!!");
    }
}
