using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    // Configs
    private int _playerInitialBubbles;
    private int _daysToWin;

    // DayEvents
    private List<DayAction> _actions;
    private List<Dialog> _dialogs;

    [ContextMenu("SetupConfigData")]
    public void SetupData()
    {
        // TODO: Replace once connection is implemented
        MockStartUp();
    }

    public int GetPlayerInitialBubbles() { return _playerInitialBubbles; }
    public int GetDaysToWin() { return _daysToWin; }
    public List<DayAction> GetActions() { return _actions; }
    public List<Dialog> GetDialogs() { return _dialogs; }

    private void MockStartUp()
    {
        // Mock game configs
        _playerInitialBubbles = 3;
        _daysToWin = 10;

        // Mock day events
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

        _dialogs = new()
        {
            CreateMockedDialog(10, "Dialog Bubble", ActionType.GAIN_BUBBLE, ActionType.LOSE_BUBBLE),
            CreateMockedDialog(20, "Dialog Charisma", ActionType.GAIN_CHARISMA, ActionType.LOSE_CHARISMA),
            CreateMockedDialog(30, "Dialog Chaos", ActionType.GAIN_CHAOS, ActionType.LOSE_CHAOS),
            CreateMockedDialog(40, "Dialog Gain Bubble or Do Nothing", ActionType.GAIN_BUBBLE, ActionType.DO_NOTHING)
        };
    }

    private Dialog CreateMockedDialog(int Id, string Text, ActionType firstActionType, ActionType secondActionType)
    {
        DialogAction firstAction = new(Id + 1, Text + " - Action 1", firstActionType);
        DialogAction secondAction = new(Id + 2, Text + " - Action 2", secondActionType);

        return new (Id, Text, firstAction, secondAction);
    }

    private void FetchFromOnlineResource()
    {
        // TODO: Implement when online spreadsheet is available
    }
}
