using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleSheetsToUnity;
using UnityEditor;
using System.Linq;

public class ConfigManager : MonoBehaviour
{
    // Configs
    private int daysToWin = 0;
    private int playerInitialBubbles = 0;
    private int playerInitialMoxie = 0;
    private int playerInitialHijinks = 0;

    // DayEvents
    private List<DayAction> actions = new();
    private List<Dialog> dialogs = new();
    private List<DialogAction> dialogActions = new();

    [SerializeField] private ConfigData _configData;

    private void Awake()
    {
        FetchFromOnlineResource();
    }

    public int GetDaysToWin() { return daysToWin; }
    public int GetPlayerInitialBubbles() { return playerInitialBubbles; }
    public int GetPlayerInitialMoxie() { return playerInitialMoxie; }
    public int GetPlayerInitialHijinks() { return playerInitialHijinks; }
    public List<DayAction> GetActions() { return actions; }
    public List<Dialog> GetDialogs() { return dialogs; }

    private void FetchFromOnlineResource()
    {
        Debug.Log("--- Retrieving data from online resource ---");
        SpreadsheetManager.Read(new GSTU_Search("1rCXe1TjigvgZ8S4XdGoupylYFF9jDkiTZqf0_bbmas0", "DaysData"), ParseSpreadsheetDayData);
        
        SpreadsheetManager.Read(new GSTU_Search("1rCXe1TjigvgZ8S4XdGoupylYFF9jDkiTZqf0_bbmas0", "PlayerData"), ParseSpreadsheetPlayerData);

    }

    private void ParseSpreadsheetDayData(GstuSpreadSheet spreadSheetRef)
    {
        List<GSTU_Cell> ids = spreadSheetRef.columns["Id"];
        List<GSTU_Cell> types = spreadSheetRef.columns["Type"];
        List<GSTU_Cell> texts = spreadSheetRef.columns["Text"];
        List<GSTU_Cell> actionTypes = spreadSheetRef.columns["ActionType"];
        List<GSTU_Cell> npcs = spreadSheetRef.columns["NPC"];

        // TODO: NTH
        List<GSTU_Cell> amount1 = spreadSheetRef.columns["Amount1"];
        List<GSTU_Cell> amount2 = spreadSheetRef.columns["Amount2"];

        // Parse file rows to arrays of objects
        for (int i = 0; i < ids.Count; i++)
        {
            if (Enum.TryParse(types[i].value, out DayEventType type))
            {
                CreateDayEventDependingOnType(type, ids[i], texts[i], actionTypes[i], npcs[i]);
            }
        }

        // Map DialogActions to Dialogs
        foreach (Dialog dialog in dialogs)
        {
            LinkDialogActionsToDialog(dialog);
        }

        _configData.dialogs = dialogs;
        _configData.actions = actions;
        _configData.dialogActions = dialogActions;
    }

    private void ParseSpreadsheetPlayerData(GstuSpreadSheet spreadSheetRef)
    {
        List<GSTU_Cell> daysToWin_data = spreadSheetRef.columns["DAYS_TO_WIN"];
        List<GSTU_Cell> init_bubbles_data = spreadSheetRef.columns["INITIAL_BUBBLES"];
        List<GSTU_Cell> init_moxie_data = spreadSheetRef.columns["INITIAL_MOXIE"];
        List<GSTU_Cell> init_hijinks_data = spreadSheetRef.columns["INITIAL_HIJINKS"];

        this.daysToWin = int.Parse(daysToWin_data[1].value);
        this.playerInitialBubbles = int.Parse(init_bubbles_data[1].value);
        this.playerInitialMoxie = int.Parse(init_moxie_data[1].value);
        this.playerInitialHijinks = int.Parse(init_hijinks_data[1].value);

        _configData.daysToWin = daysToWin;
        _configData.playerInitialMoxie = playerInitialMoxie;
        _configData.playerInitialHijinks = playerInitialHijinks;
        _configData.playerInitialBubbles = playerInitialBubbles;
    }

    private void CreateDayEventDependingOnType(DayEventType type, GSTU_Cell cellId, GSTU_Cell cellText, GSTU_Cell cellActionType, GSTU_Cell npc)
    {
        Int32.TryParse(cellId.value, out int id);
        string text = cellText.value;
        Enum.TryParse(cellActionType.value, out ActionType actionType);

        Debug.Log(id + text + npc.value);

        // Si el tipo es DIALOG
        if (type == DayEventType.DIALOG)
        {
            Dialog dialog = new Dialog();
            dialog.Initialize(id, text, null, null, npc.value);
            dialogs.Add(dialog);
        }
        // Si el tipo es DIALOG_ACTION
        else if (type == DayEventType.DIALOG_ACTION)
        {
            DialogAction dialogAction = new DialogAction();
            dialogAction.Initialize(id, text, actionType);  // Correctamente usando Initialize
            dialogActions.Add(dialogAction);
        }
        // Para otros tipos, creamos DayAction
        else
        {
            DayAction dayAction = new DayAction();
            dayAction.Initialize(id, text, actionType);  // Correctamente usando Initialize
            actions.Add(dayAction);
        }
    }

    private void LinkDialogActionsToDialog(Dialog dialog)
    {
        var dialogActions = this.dialogActions.Where(da => da.Id == dialog.Id);

        dialog.FirstAction = dialogActions.FirstOrDefault();
        dialog.SecondAction = dialogActions.LastOrDefault();

        this.dialogActions.RemoveAll(da => da.Id == dialog.Id);
    }

    private void ParseStartupConfig(GstuSpreadSheet spreadSheetRef)
    {
        List<GSTU_Cell> daysToWinCell = spreadSheetRef.columns["DAYS_TO_WIN"];
        List<GSTU_Cell> initialBubblesCell = spreadSheetRef.columns["INITIAL_BUBBLES"];
        List<GSTU_Cell> initialMoxieCell = spreadSheetRef.columns["INITIAL_MOXIE"];
        List<GSTU_Cell> initialHijinksCell = spreadSheetRef.columns["INITIAL_HIJINKS"];

        Int32.TryParse(daysToWinCell[1].value, out daysToWin);
        Int32.TryParse(initialBubblesCell[1].value, out playerInitialBubbles);
        Int32.TryParse(initialMoxieCell[1].value, out playerInitialMoxie);
        Int32.TryParse(initialHijinksCell[1].value, out playerInitialHijinks);
    }
}
