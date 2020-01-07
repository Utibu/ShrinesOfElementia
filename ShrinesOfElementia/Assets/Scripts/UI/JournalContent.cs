using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class JournalContentPage
{
    public GameObject ContentObject;
    public bool isAbilityRequired;
    public SHRINETYPES requireAbility;
}

public class JournalContent : MonoBehaviour
{

    [SerializeField] private JournalContentPage[] contentPages;
    [SerializeField] private JournalManager journalManager;
    private List<JournalContentPage> allowedPages = new List<JournalContentPage>();
    private int onIndex = -1;

    public void OnShowing()
    {
        SetAllowedPages();
        //CheckBoundaries();

        if(allowedPages.Count <= 0)
        {
            journalManager.cannotReadText.SetActive(true);
        } else
        {
            journalManager.cannotReadText.SetActive(false);
        }

        onIndex = -1;
        NextPage();
    }

    public void NextPage()
    {
        SetAllowedPages();
        onIndex++;
        foreach(JournalContentPage page in contentPages)
        {
            page.ContentObject.SetActive(false);
        }
        allowedPages[onIndex].ContentObject.SetActive(true);
        CheckBoundaries();
    }
    public void PreviousPage()
    {
        SetAllowedPages();
        onIndex--;
        foreach (JournalContentPage page in contentPages)
        {
            page.ContentObject.SetActive(false);
        }
        allowedPages[onIndex].ContentObject.SetActive(true);
        CheckBoundaries();
    }

    public void CheckBoundaries()
    {
        //Debug.Log("ALLOWED PAGES: " + allowedPages.Count);
        //Debug.Log("NEXT: " + (onIndex + 1));
        if (onIndex + 1 >= allowedPages.Count)
        {
            journalManager.nextButton.SetActive(false);
        } else
        {
            journalManager.nextButton.SetActive(true);
        }

        //Debug.Log("PREV: " + (onIndex - 1));
        if (onIndex - 1 < 0)
        {
            journalManager.prevButton.SetActive(false);
        } else
        {
            journalManager.prevButton.SetActive(true);
        }
    }

    private void SetAllowedPages()
    {
        allowedPages.Clear();
        foreach (JournalContentPage page in contentPages)
        {
            if (page.isAbilityRequired == false)
            {
                allowedPages.Add(page);
                continue;
            }
            else
            {
                AbilityManager abilities = Player.Instance.GetComponent<AbilityManager>();
                if (abilities.HasPlayerAbility(page.requireAbility))
                {
                    allowedPages.Add(page);
                    continue;
                }
            }
        }
    }

    /*private int numberOfAllowedPages()
    {
        int nr = 0;
        foreach(JournalContentPage page in contentPages)
        {
            if(page.isAbilityRequired == false)
            {
                nr++;
                break;
            } else
            {
                AbilityManager abilities = Player.Instance.GetComponent<AbilityManager>();
                if (abilities.HasPlayerAbility(page.requireAbility))
                {
                    nr++;
                    break;
                }
            }
        }
        return nr;
    }*/
}
