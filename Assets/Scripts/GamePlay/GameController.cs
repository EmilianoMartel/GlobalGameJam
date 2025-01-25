using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Button _nextDayButton;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Player _player;
    [SerializeField] private int _maxDay = 10;

    private int _currentDay = 1;
    private DayEvent _currenDayEvent;

    public Action<int> dayChangeEvent;
    public Action<bool> winGame;

    private void OnEnable()
    {
        _nextDayButton.onClick.AddListener(HandlePassDay);
        _gameManager.startGame += HandleStartGame;
    }

    private void OnDisable()
    {
        _nextDayButton.onClick.RemoveListener(HandlePassDay);
        _gameManager.startGame -= HandleStartGame;
    }

    private void HandlePassDay()
    {
        if (_currentDay > _maxDay)
        {
            winGame?.Invoke(true);
            Debug.Log("win");
        }

        _currentDay++;
        _player.DecrementBubble();
        dayChangeEvent?.Invoke(_currentDay);
    }

    private void HandleStartGame()
    {
        _player.HandleStartGame();
    }

    private void HandlePlayerDead()
    {
        winGame?.Invoke(false);
    }
}
