using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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

        _gameController.dayChangeEvent += PassDay;
    }

    private void OnDisable()
    {
        _gameController.dayChangeEvent -= PassDay;
    }

    public void PassDay(int dayNumber)
    {
        // TODO: Add check for special event
        DayAction actionToTrigger = getRandomAction();

        eventChange?.Invoke(actionToTrigger);
    }

    private DayAction getRandomAction()
    {
        int randomEvent = UnityEngine.Random.Range(0, actions.Count);

        return actions[randomEvent];
    }

    // TODO: Method for checking everything works as expected, 
    // can be removed once implementation is completed.
    [ContextMenu("LogConfigs")]
    private void LogConfigs()
    {
        StringBuilder sb = new ();
        foreach (DayAction action in actions)
        {
            sb.Append($"Id: {action.Id}, Type: {action.Type}, Text: {action.Text}, ActionType: {action.ActionType}\n");
        }

        Debug.Log("--- Actions ---");
        Debug.Log(sb.ToString());

        sb.Clear();

        foreach (Dialog dialog in dialogs)
        {
            sb.Append($"Id: {dialog.Id}, Type: {dialog.Type}, Text: {dialog.Text}, " +
                $"FirstAction.Type: {dialog.FirstAction.ActionType}, SecondAction.Type: {dialog.SecondAction.ActionType}\n");
        }

        Debug.Log("--- Dialogs ---");
        Debug.Log(sb.ToString());
    }

    public void ApplyActionTypeEffect(ActionType actionType)
    {
        switch(actionType)
        {
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
            default:
                break;
        }
    }
}
