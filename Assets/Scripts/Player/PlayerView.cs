using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerView;
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;

    [Header("Animator Parameters")]
    [SerializeField] private string _isWalkingParameter = "isWalking";
    [SerializeField] private string _isDying = "isDying";
    
    private void OnEnable()
    {
        _player.deathEvent += HandleDying;
        _player.startGame += HandleShowView;
    }

    private void OnDisable()
    {
        _player.deathEvent -= HandleDying;
        _player.startGame -= HandleShowView;
    }

    private void Awake()
    {
        _playerView.enabled = false;
        ValidateLogic();
    }

    private void HandleShowView()
    {
        _playerView.enabled = true;
    }

    private void HandleWalking(bool isWalking)
    {
        _animator.SetBool(_isWalkingParameter,isWalking);
    }

    private void HandleDying()
    {
        _animator.SetBool(_isDying, true);
    }

    private void ValidateLogic()
    {
        if (!_player)
        {
            Debug.LogError($"{name}: Player is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (!_animator)
        {
            Debug.LogError($"{name}: Animator is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
    }
}