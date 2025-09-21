using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TMP_Dropdown dpMenu;

    [HideInInspector]
    public string micOption;

    private string[] mics;
    private List<string> options;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        UpdateDropDown();
    }

    void UpdateDropDown()
    {
        //if (dpMenu != null) { Debug.LogError("DropDown Menu not assigned to UIManager"); return; }

        dpMenu.ClearOptions();

        options = new List<string>();
        mics = Microphone.devices;
        foreach (var device in mics)
        {
            options.Add(device);
        }

        dpMenu.AddOptions(options);
    }

    public void SetMicOption(int index)
    {
        micOption = (string)options[index];
        MicInput.instance.MicChange(micOption);
    }
}
