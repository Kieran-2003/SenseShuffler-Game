using UnityEngine;
using System.Collections;


public class MicInput : MonoBehaviour
{
    public static MicInput instance;

    public bool logDevices = false;

    private string _device;
    private int _sampleRate = 44100;
    private AudioSource _source;
    private AudioClip _clip;
    private int _clipLengthSec = 10;

    private void Start()
    {
        MicChange(null);
    }

    public void MicChange(string micOption)
    {
        ChooseDevice(micOption);
        GetSampleRate(_device);

        _source = GetComponent<AudioSource>();

        if (Microphone.IsRecording(_device))
        {
            Microphone.End(_device);
        }

        _clip = Microphone.Start(_device, true, _clipLengthSec, _sampleRate);

        if (_clip == null)
        {
            Debug.LogError("Microphone.Start returned a null (PERMISSION/DEVICE ISSUE).");
            return;
        }

        _source.clip = _clip;
        Debug.Log("Started Recording.");

        StartCoroutine(PlayWhenReady(_device));
    }

    void ChooseDevice(string preferredSubstring = "")
    {
        var devices = Microphone.devices;
        if(devices == null || devices.Length == 0) { return; }

        _device = string.IsNullOrEmpty(preferredSubstring) ? devices[0] : System.Array.Find(devices, d => d.Contains(preferredSubstring)) ?? devices[0];
        Debug.Log($"Chosen device: {_device}");
    }

    /// <summary>
    /// resolves the sample rate to be used for a microphone, based on its max and min frequencies.
    /// Limits the _sampleRate using Mathf.Clamp to the microphone min and max sample rates.
    /// </summary>
    /// <param name="device"></param>
    /// <returns>sample rate used for the microphone</returns>
    int GetSampleRate(string device)
    {
        Microphone.GetDeviceCaps(device, out var minFreq, out var maxFreq);

        // (0,0) means any frequency allowed
        if (minFreq == 0 || maxFreq == 0) return _sampleRate;

        if(maxFreq == 0) return _sampleRate;

        var clamped = Mathf.Clamp(_sampleRate, Mathf.Max(1, minFreq), maxFreq);

        Debug.Log($"Device caps for {device}: min = {minFreq} max = {maxFreq} using = {clamped}");

        return clamped;
    }

    IEnumerator PlayWhenReady(string device)
    {
        while (Microphone.GetPosition(device) <= 0)
        {
            yield return null;
        }
        _source.loop = true;
        _source.Play(); //  Comment out to stop live monitoring
        Debug.Log("Mic stream is now audible (monitoring).");
    }

    private void OnDisable()
    {
        if(!string.IsNullOrEmpty(_device) && Microphone.IsRecording(_device))
        {
            Microphone.End(_device);
        }
    }
}
