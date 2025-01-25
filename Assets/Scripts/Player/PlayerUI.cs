using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _charismaText;
    [SerializeField] private TMP_Text _angryText;
    [SerializeField] private List<Image> _bubbleList;

    private void OnEnable()
    {
        _player.bubblesChangeEvent += HandleChangeBubble;
        _player.angryChangeEvent += HandleAngry;
        _player.charismaChangeEvent += HandleChangeBubble;
    }

    private void OnDisable()
    {
        _player.bubblesChangeEvent -= HandleChangeBubble;
        _player.angryChangeEvent -= HandleAngry;
        _player.charismaChangeEvent -= HandleChangeBubble;
    }

    private void Awake()
    {
        ValidateLogic();
    }

    private void HandleChangeBubble(int count)
    {
        if (_bubbleList.Count > count + 1 || count < 0)
            return;

        _bubbleList[count + 1].gameObject.SetActive(false);
    }

    private void HandleAngry(int count)
    {
        _angryText.text = count.ToString();
    }

    private void HandleCharisma(int count)
    {
        _charismaText.text = count.ToString();
    }

    private void ValidateLogic()
    {
        if (!_player)
        {
            Debug.LogError($"{name}: Player is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (!_charismaText)
        {
            Debug.LogError($"{name}: Charisma Text is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (!_angryText)
        {
            Debug.LogError($"{name}: Angry Text is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (_bubbleList.Count == 0)
        {
            Debug.LogError($"{name}: Bubble List is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
    }
}