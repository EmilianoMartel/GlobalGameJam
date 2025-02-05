using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private AudioClip clip;
    [SerializeField] private SoundSFCSourcer _soundSFCSourcer;

    private string _id;
    private Button _button;

    public event Action<string> OnClick;

    private void Awake()
    {
        text ??= GetComponent<TMP_Text>();
        _button ??= GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleButtonClick);
    }

    public void Setup(string label, string id, Action<string> onClick)
    {
        text.SetText(label);
        _id = id;
        OnClick = onClick;
    }

    private void HandleButtonClick()
    {
        _soundSFCSourcer.SoundManager.PlayAudio(clip);
        OnClick?.Invoke(_id);
    }
}
