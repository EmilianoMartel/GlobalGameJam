using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Button _nextDayButton;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private ConfigManager _configManager;
    [SerializeField] private Player _player;
    [SerializeField] private int _maxDay = 10;
    [SerializeField] private float _waitForStart = 0.5f;

    private int _currentDay = 1;

    public Action<int> dayChangeEvent;
    public Action<bool> winGame;

    private void OnEnable()
    {
        _nextDayButton.onClick.AddListener(HandlePassDay);
        _gameManager.startGame += HandleStartGame;
        _player.deathEvent += HandlePlayerDead;
    }

    private void OnDisable()
    {
        _nextDayButton.onClick.RemoveListener(HandlePassDay);
        _gameManager.startGame -= HandleStartGame;
        _player.deathEvent -= HandlePlayerDead;
    }

    private void SetupInitialData()
    {
        int configMaxDay = _configManager.GetDaysToWin();

        if (configMaxDay > 0)
        {
            _maxDay = configMaxDay;
        }
    }

    private void HandlePassDay()
    {
        if (_currentDay >= _maxDay)
        {
            winGame?.Invoke(true);
            Debug.Log("win");
            _gameManager.EndGame();
        } else
        {
            _currentDay++;
            if (_currentDay % 5 == 0)
            {
                _player.DecrementBubble();
            }
            dayChangeEvent?.Invoke(_currentDay);
        }
    }

    private void HandleStartGame()
    {
        ResetStatus();
        _player.HandleStartGame();
        StartCoroutine(WaitingStart());
    }

    private IEnumerator WaitingStart()
    {
        yield return new WaitForSeconds(_waitForStart);
        dayChangeEvent?.Invoke(_currentDay);
    }

    private void HandlePlayerDead()
    {
        Debug.Log("loose");
        winGame?.Invoke(false);
        _gameManager.EndGame();
    }

    private void ResetStatus()
    {
        _currentDay = 1;
    }
}
