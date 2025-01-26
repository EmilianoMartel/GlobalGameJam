using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayUI : MonoBehaviour
{
    // Dependencies
    [SerializeField] private GameController _gameController;
    [SerializeField] private DayManager _dayManager;

    // UI elements
    [SerializeField] private TMP_Text _dayNumberText;
    [SerializeField] private TMP_Text _dayEventText;
    [SerializeField] private Button _optionAButton;
    [SerializeField] private Button _optionBButton;

    private void OnEnable()
    {
        _gameController.dayChangeEvent += ChangeDayNumber;
        _dayManager.eventChange += ChangeDayEvent;
    }

    private void OnDisable()
    {
        _gameController.dayChangeEvent -= ChangeDayNumber;
        _dayManager.eventChange -= ChangeDayEvent;
    }

    private void ChangeDayNumber(int dayNumber)
    {
        _dayNumberText.text = "Day " + dayNumber.ToString();
    }

    private void ChangeDayEvent(DayEvent dayEvent)
    {
        _dayEventText.text = dayEvent.Text;

        if (dayEvent.Type == DayEventType.DIALOG)
        {
            Dialog dialog = (Dialog) dayEvent;
            
            // Option A
            _optionAButton.GetComponentInChildren<TMP_Text>().text = dialog.FirstAction.Text;
            _optionAButton.onClick.AddListener(() => { _dayManager.ApplyActionTypeEffect(dialog.FirstAction.ActionType); });

            // Option B
            _optionBButton.GetComponentInChildren<TMP_Text>().text = dialog.SecondAction.Text;
            _optionBButton.onClick.AddListener(() => { _dayManager.ApplyActionTypeEffect(dialog.SecondAction.ActionType); });
        }
    }
}
