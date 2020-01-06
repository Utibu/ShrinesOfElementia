using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckpointUIText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float showDuration = 3.0f;

    private void Start()
    {
        EventManager.Instance.RegisterListener<CheckpointEvent>(OnCheckpoint);
    }

    private void OnCheckpoint(CheckpointEvent eve)
    {
        ShowText();
        TimerManager.Instance.SetNewTimer(gameObject, showDuration, HideText);
    }

    private void ShowText()
    {
        text.gameObject.SetActive(true);
    }

    private void HideText()
    {
        text.gameObject.SetActive(false);
    }
}
