using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChargingPanel : MonoBehaviour
{
    [SerializeField] private float _await = 1.5f;
    [SerializeField] private GameObject _panelBackground;
    [SerializeField] private Slider loadingBarFill;
    [SerializeField] private GameManager _gameManager;

    private float _currenTime = 0;

    private void OnEnable()
    {
        _gameManager.startGame += HandleStarGame;
    }

    private void OnDisable()
    {
        _gameManager.startGame -= HandleStarGame;
    }

    private void HandleStarGame()
    {
        _panelBackground.SetActive(true);
       _currenTime = 0;
        loadingBarFill.value = 1;
        StartCoroutine(HandleDowloadBar());
    }

    private IEnumerator HandleDowloadBar()
    {
        while (_currenTime <= _await)
        {
            _currenTime += Time.deltaTime;
            loadingBarFill.value = _currenTime / _await;
            yield return null;
        }
        _panelBackground.SetActive(false);
    }
}
