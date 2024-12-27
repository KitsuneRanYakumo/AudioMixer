using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SwitcherSound : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    private float _maxVolumeMixer = 0;
    private float _minVolumeMixer = -80;

    public bool IsOff => _toggle.isOn;
    
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
        _audioMixerGroup.audioMixer.SetFloat(_audioMixerGroup.name, _maxVolumeMixer);
    }

    private void OffSound()
    {
        _audioMixerGroup.audioMixer.SetFloat(_audioMixerGroup.name, _minVolumeMixer);
    }
}