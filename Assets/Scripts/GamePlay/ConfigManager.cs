using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleSheetsToUnity;

public class ConfigManager : MonoBehaviour
{

    [SerializeField] private DataRetrieve dayData;
    // Configs
    private int _playerInitialBubbles;
    private int _daysToWin;

    // DayEvents
    private List<DayAction> _actions;
    private List<Dialog> _dialogs_available;
    private List<Dialog> _dialogs;

    [ContextMenu("SetupConfigData")]
    public void SetupData()
    {
        // TODO: Replace once connection is implemented
        //MockStartUp();
        //FetchFromOnlineResource();
        CreateDays();
    }

    public int GetPlayerInitialBubbles() { return _playerInitialBubbles; }
    public int GetDaysToWin() { return _daysToWin; }
    public List<DayAction> GetActions() { return _actions; }
    public List<Dialog> GetDialogs() { return _dialogs; }


    private void CreateDays(){
        _playerInitialBubbles = 3;
        _daysToWin = 10;

        _actions = new()
        {
            new(1, "Gain bubble", ActionType.GAIN_BUBBLE),
            new(2, "Lose bubble", ActionType.LOSE_BUBBLE),
            new(3, "Gain charisma", ActionType.GAIN_CHARISMA),
            new(4, "Lose charisma", ActionType.LOSE_CHARISMA),
            new(5, "Gain chaos", ActionType.GAIN_CHAOS),
            new(6, "Lose chaos", ActionType.LOSE_CHAOS),
            new(7, "Do nothing", ActionType.DO_NOTHING)
        };

        _dialogs_available = new();
        _dialogs = new();
        
        foreach ( var day in dayData.days_list ){            
            var responseAction1 = Enum.Parse<ActionType>(day.ResponseAction1);
            var responseAction2 = Enum.Parse<ActionType>(day.ResponseAction2);
            var day_obj = CreateDialog( day.Id, day.Text, responseAction1, responseAction2 );
            _dialogs_available.Add(day_obj);
        }

        for (var i = 0; i < 10; i++){
            
            var r = UnityEngine.Random.Range(0, _dialogs_available.Count);
            var _dialog_type = _dialogs_available[r];
            Debug.Log(_dialog_type.Text);
            _dialogs.Add( _dialog_type );
        }
    }

    private Dialog CreateDialog(int Id, string Text, ActionType firstActionType, ActionType secondActionType)
    {
        DialogAction firstAction = new(Id + 1, Text + " - Action 1", firstActionType);
        DialogAction secondAction = new(Id + 2, Text + " - Action 2", secondActionType);

        return new (Id, Text, firstAction, secondAction);
    }
}
