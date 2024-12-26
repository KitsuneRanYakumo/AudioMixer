using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonContainer : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private AudioClip _audioClip;

    public event Action<AudioClip> ButtonPressed;

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleEventButton);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleEventButton);
    }

    private void HandleEventButton()
    {
        ButtonPressed?.Invoke(_audioClip);
    }
}