using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; //Required if using TMPro for UI elements

public class DateTime4UI : MonoBehaviour {

    //Variable references for UI elements
    public GameObject hourUI;
    public GameObject dateUI;
    private TMP_Text hourText;
    private TMP_Text dateText;

	void Start () {
        //Initialise the UI elements
        hourUI = GameObject.Find("Time");
        dateUI = GameObject.Find("Date");

        //(Un)comment this code for use with TMPro
        hourText = hourUI.GetComponent<TextMeshProUGUI>();
        dateText = dateUI.GetComponent<TextMeshProUGUI>();
        
        //OR
        
        //(Un)comment this code for use with regular Unity UI text elements
        //hourText = hourUI.GetComponent<Text>();
        //dateText = dateUI.GetComponent<Text>();
    }
	
	void Update () {
        //Get the current date and time
        DateTime dtValue = DateTime.Now;
        //Set date time format and pass to UI 
        hourText.SetText(dtValue.ToString("h:mm tt").ToLower());
        dateText.SetText(dtValue.ToString("dddd, d MMMM, yyyy"));
    }
}
