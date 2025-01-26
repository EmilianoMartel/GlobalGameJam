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

    public event Action startGame;
    public event Action endGame;

    public Dialog currentDialog;

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

        if (configManager == null)
        {
            Debug.LogWarning($"{name}: ConfigManager not set up");
        } 
        else 
        {
            Debug.Log("Setting up ConfigManager...");
            configManager.SetupData();
            _playerInitialBubbles = configManager.GetPlayerInitialBubbles();
            _daysToWin = configManager.GetDaysToWin();
            Debug.Log("ConfigManager set up successfully.");
        }
    }
}