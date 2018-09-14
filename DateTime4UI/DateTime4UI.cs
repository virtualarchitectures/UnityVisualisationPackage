using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Comment this code if not using Text Mesh Pro 
using TMPro;

public class DateTime4UI : MonoBehaviour {

    //Variable references for UI elements
    public GameObject hourUI;
    public GameObject dateUI;

    //Comment this code if using regular Unity UI text elements
    private TMP_Text hourText;
    private TMP_Text dateText;


    void Start ()
    {
        //Comment this code if using regular Unity UI text elements
        hourText = hourUI.GetComponent<TextMeshProUGUI>();
        dateText = dateUI.GetComponent<TextMeshProUGUI>();        
    }
	
	void Update ()
    {
        //Get the current date and time
        DateTime dtValue = DateTime.Now;

        //Set date time format and pass to UI 

        //Comment this code if using regular Unity UI text elements
        hourText.SetText(dtValue.ToString("h:mm tt").ToLower());
        dateText.SetText(dtValue.ToString("dddd, d MMMM, yyyy"));

        //Uncomment this code for use with regular Unity UI text elements
        //hourUI.GetComponent<Text>().text = dtValue.ToString("h:mm tt").ToLower();
        //dateUI.GetComponent<Text>().text = dtValue.ToString("dddd, d MMMM, yyyy");
    }
}
