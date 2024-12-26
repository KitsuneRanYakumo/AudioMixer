using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundMenu : MonoBehaviour
{
    [SerializeField] private SliderContainer[] _sliderContainers;
    [SerializeField] private ButtonContainer[] _buttonContainers;
    [SerializeField] private Toggle _muteAllSoundsToggle;

    public event Action<string, float, float> SliderContianerSentMessage;
    public event Action<AudioClip> ButtonContainerSentAudioClip;
    public event Action<bool> MuteAllSoundsToggleChanged;

    private void OnEnable()
    {
        SubscribeUIElements();
    }

    private void OnDisable()
    {
        UnsubscribeUIElements();
    }

    private void HandleEventSliderContainer(string nameSlider, float value, float maxValue)
    {
        SliderContianerSentMessage?.Invoke(nameSlider, value, maxValue);
    }

    private void HandleEventButtonContainer(AudioClip clip)
    {
        ButtonContainerSentAudioClip?.Invoke(clip);
    }

    private void HandleEventMuteAllSoundToggle(bool state)
    {
        MuteAllSoundsToggleChanged?.Invoke(state);
    }

    private void SubscribeUIElements()
    {
        foreach (var sliderContainer in _sliderContainers)
            sliderContainer.SliderChanged += HandleEventSliderContainer;

        foreach (var buttonContainer in _buttonContainers)
            buttonContainer.ButtonPressed += HandleEventButtonContainer;

        _muteAllSoundsToggle.onValueChanged.AddListener(HandleEventMuteAllSoundToggle);
    }

    private void UnsubscribeUIElements()
    {
        foreach (var sliderContainer in _sliderContainers)
            sliderContainer.SliderChanged -= HandleEventSliderContainer;

        foreach (var buttonContainer in _buttonContainers)
            buttonContainer.ButtonPressed -= HandleEventButtonContainer;

        _muteAllSoundsToggle.onValueChanged.RemoveListener(HandleEventMuteAllSoundToggle);
    }
}