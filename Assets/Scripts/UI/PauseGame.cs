using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _panel;

    private bool _isActive = false;

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleActivePanel);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleActivePanel);
    }

    private void HandleActivePanel()
    {
        _isActive = !_isActive;
        _panel.SetActive(_isActive);
    }
}