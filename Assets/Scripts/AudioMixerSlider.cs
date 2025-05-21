using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerSlider : MonoBehaviour
{
    private const float DisabledVolume = -80;
    public Slider volumeSlider;
    public AudioMixer audioMixer;
    public string mixerParameter;
    public float minimumVolume;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volumeSlider.SetValueWithoutNotify(GetMixerVolume());
    }
    
    public void UpdateMixerVolume(float volumeValue)
    {
        SetMixerVolume(volumeValue);
    }

    private void SetMixerVolume(float volumeValue)
    {
        var mixerVolume = (volumeValue == 0)
            ? DisabledVolume
            : Mathf.Lerp(minimumVolume, 0, volumeValue);
        audioMixer.SetFloat(mixerParameter, mixerVolume);
    }
    
    private float GetMixerVolume()
    {
        audioMixer.GetFloat(mixerParameter, out var mixerVolume);
        return Math.Abs(mixerVolume - DisabledVolume) < 1e-10
            ? 0
            : Mathf.Lerp(1, 0, mixerVolume / minimumVolume);
    }
}
