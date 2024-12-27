using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SwitcherSound : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    public bool IsOff => _toggle.isOn;

    private string _nameExposedParameter = Converter.EnumToString(ExposedParameters.MasterVolume);
    private float _maxVolumeMixer = 0;
    private float _minVolumeMixer = -80;

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(SwitchSound);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(SwitchSound);
    }

    private void SwitchSound(bool state)
    {
        if (state)
            OnSound();
        else
            OffSound();
    }

    private void OnSound()
    {
        _audioMixerGroup.audioMixer.SetFloat(_nameExposedParameter, _maxVolumeMixer);
    }

    private void OffSound()
    {
        _audioMixerGroup.audioMixer.SetFloat(_nameExposedParameter, _minVolumeMixer);
    }
}