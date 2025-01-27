using UnityEngine;
using System;

public class Dialog : DayEvent
{
    public DialogAction FirstAction { get; set; }
    public DialogAction SecondAction { get; set; }

    public int NPC_ID;

    public Dialog(int Id, string Text, DialogAction FirstAction, DialogAction SecondAction, string npc_id) : base(Id, DayEventType.DIALOG, Text)
    {
        this.FirstAction = FirstAction;
        this.SecondAction = SecondAction;

        if (npc_id != "None"){
            this.NPC_ID = int.Parse(npc_id);
        }
    }
}
