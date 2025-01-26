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

    private List<DayEvent> _dayEventsAvailable;
    private List<DayEvent> _dayEvents;

    [SerializeField] private DayFactory dayFactory;

    [ContextMenu("SetupConfigData")]
    public void SetupData()
    {
        CreateDays();
    }

    public int GetPlayerInitialBubbles() { return _playerInitialBubbles; }
    public int GetDaysToWin() { return _daysToWin; }
    public List<DayAction> GetActions() { return _actions; }
    public List<Dialog> GetDialogs() { return _dialogs; }

    public List<DayEvent> GetDayEvents() { return _dayEvents; }


    private void CreateDays(){

        _playerInitialBubbles = 3;
        _daysToWin = 10;

        _actions = new()
        {
            new(1, "Gain bubble", ActionType.GAIN_BUBBLE),
            new(2, "Lose bubble", ActionType.LOSE_BUBBLE),
            new(3, "Gain charisma", ActionType.GAIN_MOXIE),
            new(5, "Gain chaos", ActionType.GAIN_HIJINKS),
            new(6, "Lose chaos", ActionType.LOSE_HIJINKS),
            new(7, "Do nothing", ActionType.DO_NOTHING)
        };

        _dialogs_available = new();
        _dialogs = new();

        _dayEventsAvailable = new();
        _dayEvents = new();

        

        //GROUP DAYS BY ID
        var groupedDays = new List<Day>[dayData.last_days_amount];
        for (var j=0; j < dayData.last_days_amount; j++){
            groupedDays[j] = new List<Day>();
        }


        //ADD LISTS OF DAYS TO GROUPED BY ID LIST
        var last_Day_ID = 0;
        foreach ( var day in dayData.days_list ){ 
            groupedDays[day.Day_ID - 1].Add( day );
            last_Day_ID = day.Day_ID;
        }

        //CREATE LIST OF AVAILABLE OPTIONS FOR INSTANCING DAYS
        foreach ( var days in groupedDays){
            _dayEventsAvailable.Add( dayFactory.GetDialogFromDay( Enum.Parse<DayEventType>(days[0].Type), days) );
        }

        // RANDOMIZE AS MANY DAYS AS NEEDED FOR WINNING
        for (var i = 0; i < _daysToWin; i++){
            var r = UnityEngine.Random.Range(0, last_Day_ID);
            _dayEvents.Add( _dayEventsAvailable[r] );
        }
    }
}
