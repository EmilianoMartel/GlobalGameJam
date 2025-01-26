using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayFactory : MonoBehaviour
{
    public DayEvent GetDialogFromDay( DayEventType day_type, List<Day> dayData  ){

        DayEvent dayEvent;
        switch (day_type)
        {
            default:
            case DayEventType.ACTION:
                dayEvent = new DayAction( dayData[0].Id, dayData[0].Text, Enum.Parse<ActionType>(dayData[0].ActionType));
                break;
            case DayEventType.DIALOG:
                var firstActionType = Enum.Parse<ActionType>(dayData[1].ResponseAction1);
                var secondActionType = Enum.Parse<ActionType>(dayData[2].ResponseAction2);

                DialogAction firstAction = new(dayData[1].Id, dayData[1].Text, firstActionType);
                DialogAction secondAction = new(dayData[2].Id, dayData[2].Text, secondActionType);

                dayEvent = new Dialog(dayData[0].Day_ID, dayData[0].Id, dayData[0].Text, firstAction, secondAction ); 
                break;
        }

        return dayEvent;
        //var dialog = new Dialog( day.Day_ID, day.Id, day.Text)
    }
}
