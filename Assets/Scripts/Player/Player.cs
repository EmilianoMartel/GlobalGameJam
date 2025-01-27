using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ConfigManager _configManager;
    [SerializeField] private ConfigData _configData;

    [Header("Stats")]
    private int _maxBubbles = 3;
    private int _startCharisma = 10;
    private int _startAngry = 10;

    [Space(10)]
    [Header("Player awaits")]
    [SerializeField] private float _waitForDead = .5f;
    [SerializeField] private float _waitForStart = 0.2f;

    private int _currentBubbles;
    private int _currentCharisma;
    private int _currentAngry;

    public Action startGame;
    public Action<int> bubblesChangeEvent;
    public Action<int> charismaChangeEvent;
    public Action<int> angryChangeEvent;
    public Action deathEvent;

    public Action starAnimationDeath;

    private void Start()
    {
        SetupInitialData();
    }

    public void HandleStartGame()
    {
        StartCoroutine(WaitingStart());
    }

    private void SetupInitialData()
    {
        Debug.Log("Setting up player initial data...");
        //TODO: Agregar bubbles
        if (_configManager.GetPlayerInitialBubbles() != 0)
        {
            int configStartMoxie = _configManager.GetPlayerInitialMoxie();

            if (configStartMoxie > 0)
            {
                _startCharisma = configStartMoxie;
            }

            int configStartHijinks = _configManager.GetPlayerInitialHijinks();

            if (configStartMoxie > 0)
            {
                _startAngry = configStartHijinks;
            }

            _maxBubbles = _configManager.GetPlayerInitialBubbles();
        }
        else
        {
            _startCharisma = _configData.playerInitialMoxie;
            _startAngry = _configData.playerInitialHijinks;
            _maxBubbles = _configData.playerInitialBubbles;
        }


        Debug.Log($"Setup complete. _startCharisma = {_startCharisma}, _startAngry = {_startAngry}");
    }

    private IEnumerator WaitingStart()
    {
        startGame?.Invoke();
        _currentAngry = _startAngry;
        _currentBubbles = _maxBubbles;
        _currentCharisma = _startCharisma;
        yield return new WaitForSeconds(_waitForStart);
     
        Debug.Log( "PLAYER WAITING START" );

        bubblesChangeEvent?.Invoke(_currentBubbles);
        angryChangeEvent?.Invoke(_currentAngry);
        charismaChangeEvent?.Invoke(_currentCharisma);
    }

    public void IncrementBubble()
    {
        _currentBubbles++;
        bubblesChangeEvent?.Invoke(_currentBubbles);
    }

    public void DecrementBubble()
    {
        _currentBubbles--;
        bubblesChangeEvent?.Invoke(_currentBubbles);
        if (_currentBubbles <= 0)
            StartCoroutine(DeadLogic());
    }

    public void UpdateMoxie(int moxie)
    {
        _currentCharisma += moxie;

        if (_currentCharisma <= 0)
            _currentCharisma = 0;

        charismaChangeEvent?.Invoke(_currentCharisma);
    }

    public void UpdateHijinks(int hijinks)
    {
        _currentAngry += hijinks;

        if (_currentAngry <= 0)
            _currentAngry = 0;

        angryChangeEvent?.Invoke(_currentAngry);
    }

    private IEnumerator DeadLogic()
    {
        starAnimationDeath?.Invoke();
        yield return new WaitForSeconds(_waitForDead);
        deathEvent?.Invoke();
    }
}