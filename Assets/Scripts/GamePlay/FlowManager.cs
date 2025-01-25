using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FlowManager : MonoBehaviour
{
    [SerializeField] private string playId = "Play";
    [SerializeField] private string exitId = "Exit";

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
    }
}