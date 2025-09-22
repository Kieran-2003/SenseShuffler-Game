using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel.Design;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Heeader("Dropdown Menu")]
    [ToolTip("Reference to the dropdown gameobject witin the heirarchy")]   
    public TMP_Dropdown dpMenu;

    [HideInInspector]
    public string micOption;

    private string[] mics;
    private List<string> options;   // List<string> object to work with TMP_Dropdown Object.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(instance == null)
        {
            this = instance;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        UpdateDropDown();   // May need to be invoked later; Invoke("UpdateDropDown", 1f);
    }

    /// <summary>
    /// Method to update the dropdown menu with available microphone options.
    /// </summary>
    void UpdateDropDown()
    {
        if (dpMenu == null) { Debug.LogError("DropDown Menu not assigned to UIManager"); return; }  //  Error handling

        dpMenu.ClearOptions();  //  Clear existing options in the dropdown menu.

        //  iterate through array of mics and add to the List<string> options.
        options = new List<string>();
        mics = Microphone.devices;
        foreach (var device in mics)
        {
            options.Add(device);
        }

        dpMenu.AddOptions(options);
    }

    /// <summary>
    /// Sets the microphone option based on the specified index.
    /// </summary>
    /// <remarks>This method updates the current microphone option and notifies the microphone input system of
    /// the change. Ensure that the index corresponds to a valid option in the available list.</remarks>
    /// <param name="index">The zero-based index of the microphone option to select. Must be within the bounds of the available options.</param>
    public void SetMicOption(int index)
    {
        micOption = (string)options[index];

        Debug.log("Selected mic: " + micOption);

        MicInput.instance.MicChange(micOption);
    }
}
