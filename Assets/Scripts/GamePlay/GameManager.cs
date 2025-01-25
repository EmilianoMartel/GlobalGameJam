using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string playId = "Play";
    [SerializeField] private string exitId = "Exit";

    // Dependencies
    [SerializeField] private ConfigManager configManager;

    // Configs
    private int _playerInitialBubbles;
    private int _daysToWin;
    private List<DayAction> _actions;
    private List<Dialog> _dialogs;

    public event Action startGame;

    public void HandleSpecialEvents(string id)
    {
        if (id == playId)
            StartGame();

        if (id == exitId)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }

    [ContextMenu("Start")]
    private void StartGame()
    {
        startGame?.Invoke();

        if (configManager != null)
        {
            Debug.Log("Setting up ConfigManager...");
            configManager.SetupData();
            _playerInitialBubbles = configManager.GetPlayerInitialBubbles();
            _daysToWin = configManager.GetDaysToWin();
            _actions = configManager.GetActions();
            _dialogs = configManager.GetDialogs();
            Debug.Log("ConfigManager set up successfully.");
        }
    }

    // TODO: Method for checking everything works as expected, 
    // can be removed once implementation is completed.
    [ContextMenu("LogConfigs")]
    private void LogConfigs()
    {
        Debug.Log("------------ Config values ------------");
        Debug.Log($"Player initial bubbles: {_playerInitialBubbles}");
        Debug.Log($"Days to win: {_daysToWin}");

        Debug.Log("--- Actions ---");
        foreach (DayAction action in _actions) 
        {
            Debug.Log($"Id: {action.Id}, Type: {action.Type}, Text: {action.Text}, ActionType: {action.ActionType}");
        }

        Debug.Log("--- Dialogs ---");
        foreach (Dialog dialog in _dialogs)
        {
            Debug.Log($"Id: {dialog.Id}, Type: {dialog.Type}, Text: {dialog.Text}, " +
                $"FirstAction.Type: {dialog.FirstAction.ActionType}, SecondAction.Type: {dialog.SecondAction.ActionType}");
        }
    }
}