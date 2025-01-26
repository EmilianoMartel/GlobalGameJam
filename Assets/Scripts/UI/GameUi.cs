using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUi : MonoBehaviour
{
    [SerializeField] private GameController _gameController;

    [SerializeField] private TMP_Text _dayText;

    private void OnEnable()
    {
        _gameController.dayChangeEvent += HandleDayChange;
    }

    private void OnDisable()
    {
        _gameController.dayChangeEvent -= HandleDayChange;
    }

    private void HandleDayChange(int day, Dialog dialog)
    {
        _dayText.text = "Day " + day.ToString() + "\n" + dialog.Text;


    }
}
