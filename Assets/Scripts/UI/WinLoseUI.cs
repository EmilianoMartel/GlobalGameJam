using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinLoseUI : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private TMP_Text _finalText;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _loosePanel;
    [SerializeField] private string _winText = "What a nice trip on with out Bubble Trouble";
    [SerializeField] private string _loseText = "What a Bubble Touble! Best luck next time";

    private void OnEnable()
    {
        _gameController.winGame += HandleWinGame;
    }

    private void OnDisable()
    {
        _gameController.winGame -= HandleWinGame;
    }

    private void HandleWinGame(bool isWin)
    {
        if (isWin)
        {
            _winPanel.SetActive(true);
            _loosePanel.SetActive(false);
            _finalText.text = _winText;
        }
        else
        {
            _winPanel.SetActive(false);
            _loosePanel.SetActive(true);
            _finalText.text = _loseText;
        }
    }
}
