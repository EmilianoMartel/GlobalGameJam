using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    // Dependencies
    [SerializeField] private ConfigManager _configManager;
    [SerializeField] private GameController _gameController;

    // Player
    [SerializeField] private Player _player;

    private List<DayAction> actions = new();
    private List<Dialog> dialogs = new();

    public Action<DayEvent> eventChange;

    private void Start()
    {
        _gameController.dayChangeEvent += PassDay;
    }

    private void OnEnable()
    {
        if (_configManager == null)
        {
            Debug.LogWarning($"{name}: ConfigManager not set up");
        }
        else
        {
            actions = _configManager.GetActions();
            dialogs = _configManager.GetDialogs();
        }
    }

    private void OnDisable()
    {
        _gameController.dayChangeEvent -= PassDay;
    }

    public void PassDay(int dayNumber)
    {
        DayEvent actionToTrigger = null;
        // TODO: Add check for special event
        if(dayNumber % 3 == 0)
        {
            actionToTrigger = getRandomSpecialEvent();
        } else
        {
            DayAction randomEvent = getRandomAction();
            ApplyActionTypeEffect(randomEvent.ActionType);

            actionToTrigger = randomEvent;
        }

        eventChange?.Invoke(actionToTrigger);
    }

    private DayAction getRandomAction()
    {
        int randomEvent = UnityEngine.Random.Range(0, actions.Count);

        return actions[randomEvent];
    }

    private Dialog getRandomSpecialEvent()
    {
        int randomEvent = UnityEngine.Random.Range(0, dialogs.Count);

        Debug.Log(dialogs.Count);
        return dialogs[randomEvent];
    }

    public void ApplyActionTypeEffect(ActionType actionType)
    {
        Debug.Log($"ApplyActionTypeEffect triggered: {actionType}");

        switch(actionType)
        {
            //SIMPLES
            case ActionType.GAIN_BUBBLE:
                _player.IncrementBubble();
                break;
            case ActionType.LOSE_BUBBLE:
                _player.DecrementBubble();
                break;
            case ActionType.GAIN_MOXIE:
                _player.UpdateMoxie(1);
                break;
            case ActionType.LOSE_MOXIE:
                _player.UpdateMoxie(-1);
                break;
            case ActionType.GAIN_HIJINKS:
                _player.UpdateHijinks(1);
                break;
            case ActionType.LOSE_HIJINKS:
                _player.UpdateHijinks(-1);
                break;
                
            //GAIN BUBBLES
            case ActionType.GAIN_BUBBLE_GAIN_HIJINKS:
                _player.IncrementBubble();
                _player.UpdateHijinks(1);
                break;
            case ActionType.GAIN_BUBBLE_GAIN_MOXIE:
                _player.IncrementBubble();
                _player.UpdateMoxie(1);
                break;
            case ActionType.GAIN_BUBBLE_LOSE_HIJINKS:
                _player.IncrementBubble();
                _player.UpdateHijinks(-1);
                break;
            case ActionType.GAIN_BUBBLE_LOSE_MOXIE:
                _player.IncrementBubble();
                _player.UpdateMoxie(-1);
                break;

            //LOSE BUBBLES
            case ActionType.LOSE_BUBBLE_GAIN_HIJINKS:
                _player.DecrementBubble();
                _player.UpdateHijinks(1);
                break;
            case ActionType.LOSE_BUBBLE_GAIN_MOXIE:
                _player.DecrementBubble();
                _player.UpdateMoxie(1);
                break;
            case ActionType.LOSE_BUBBLE_LOSE_HIJINKS:
                _player.DecrementBubble();
                _player.UpdateHijinks(-1);
                break;
            case ActionType.LOSE_BUBBLE_LOSE_MOXIE:
                _player.DecrementBubble();
                _player.UpdateMoxie(-1);
                break;
            
            //NON-BUBBLE
            case ActionType.GAIN_MOXIE_GAIN_HIJINKS:
                _player.UpdateHijinks(1);
                _player.UpdateMoxie(1);
                break;
            case ActionType.LOSE_MOXIE_LOSE_HIJINKS:
                _player.UpdateHijinks(-1);
                _player.UpdateMoxie(-1);
                break;
            case ActionType.GAIN_HIJINKS_LOSE_MOXIE:
                _player.UpdateHijinks(1);
                _player.UpdateMoxie(-1);
                break;
            case ActionType.GAIN_MOXIE_LOSE_HIJINKS:
                _player.UpdateHijinks(-1);
                _player.UpdateMoxie(1);
                break;

            default:
                break;
        }
    }
}
