using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class settingScript : MonoBehaviour {

    public Button ON;
    public Button OFF;
    
    public Button Return;

    // Use this for initialization
    void Start()
    {
        ON = ON.GetComponent<Button>();
        OFF = OFF.GetComponent<Button>();
      
        Return = Return.GetComponent<Button>();
    }
    public void ReturnMenu()
    {
        Application.LoadLevel(0);
    }
}