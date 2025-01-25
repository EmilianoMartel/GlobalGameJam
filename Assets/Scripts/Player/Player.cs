using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int _maxBubbles = 3;
    [SerializeField] private int _startCharisma = 10;
    [SerializeField] private int _startAngry = 10;

    [Space(10)]
    [Header("Player awaits")]
    [SerializeField] private float _waitForDead = 1.5f;

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

    public void ModifyCharisma(int charisma)
    {
        _currentCharisma += charisma;
        charismaChangeEvent?.Invoke(_currentCharisma);
    }

    public void ModifyAngry(int angry)
    {
        _currentAngry += angry;
        angryChangeEvent?.Invoke(_currentAngry);
    }

    private IEnumerator DeadLogic()
    {
        starAnimationDeath?.Invoke();
        yield return new WaitForSeconds(_waitForDead);
        deathEvent?.Invoke();
    }
}