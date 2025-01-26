using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    private int _maxBubbles = 3;
    private int _startCharisma = 10;
    private int _startAngry = 10;


    [SerializeField] private ConfigManager configManager;

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
        _currentBubbles = _maxBubbles;
        _currentCharisma = _startCharisma;
        _currentAngry = _startAngry;

        bubblesChangeEvent?.Invoke(_currentBubbles);
        angryChangeEvent?.Invoke(_currentAngry);
        charismaChangeEvent?.Invoke(_currentCharisma);
    }

    public void HandleStartGame()
    {
        startGame?.Invoke();

        StartCoroutine(WaitingStart());
    }

    private IEnumerator WaitingStart()
    {
        yield return new WaitForSeconds(_waitForStart);

        // lo tuve que poner aca por que en HandleStartGame todavía estan los valores default
        _currentBubbles = configManager.GetPlayerInitialBubbles();
        _currentCharisma = configManager.GetPlayerInitialMoxie();
        _currentAngry = configManager.GetPlayerInitialHijinks();

     
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