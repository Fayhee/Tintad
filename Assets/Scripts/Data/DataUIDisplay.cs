using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataUIDisplay : MonoBehaviour
{
    [SerializeField]
     DayData M_Data;

    [SerializeField]
    TMP_Text FMText;

    [SerializeField]
    TMP_Text FNText;

    const string k_DefaultText = "Pollutant Management: "; 
    
    public enum Day
    {
        June_1st,
        June_2nd,
        June_3rd,
        June_4th,
        June_5th

    }

    public Day day;

    void Update()
    {
        if (M_Data.DataCaptured)
        {
            FMText.text = k_DefaultText + day.ToString()+ ": " + M_Data.Getaqi(day).ToString();
        }
    }
    
}
