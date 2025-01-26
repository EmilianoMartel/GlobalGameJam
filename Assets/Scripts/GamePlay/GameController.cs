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
    [SerializeField] private float _waitForStart = 0.5f;

    private int _currentDay = 1;
    private DayEvent _currenDayEvent;

    public Action<int, DayEvent> dayChangeEvent;
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

    private void HandlePassDay()
    {
        if (_currentDay > _maxDay)
        {
            winGame?.Invoke(true);
            Debug.Log("win");
        }

        _currentDay++;
        var day = _gameManager.GetCurrentDay(_currentDay);
        _player.DecrementBubble();
        dayChangeEvent?.Invoke(_currentDay, day);
    }

    private void HandleStartGame()
    {
        _currentDay = 1;
        _player.HandleStartGame();
        StartCoroutine(WaitingStart());
    }

    private IEnumerator WaitingStart()
    {
        yield return new WaitForSeconds(_waitForStart);
        dayChangeEvent?.Invoke(_currentDay, _gameManager.GetCurrentDay(_currentDay) );
    }

    private void HandlePlayerDead()
    {
        Debug.Log("loose");
        winGame?.Invoke(false);
    }
}
