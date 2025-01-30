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
    }

    public void EndGame()
    {
        endGame?.Invoke();
    }
}