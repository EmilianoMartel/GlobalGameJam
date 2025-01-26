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
    [SerializeField] private List<BubbleUI> _bubbleList;

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
        for (int i = 0; i < _bubbleList.Count; i++)
        {
            if (i == count)
            {
                _bubbleList[i].DesactivateBubble();
            }
            else if(i < count)
            {
                _bubbleList[i].ActivateBubble();
            }
        }
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