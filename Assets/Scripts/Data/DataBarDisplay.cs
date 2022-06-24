using System;
using TMPro;
using UnityEngine;

public class DataBarDisplay : MonoBehaviour
{
    [SerializeField]
    DayData M_Data;

    [SerializeField]
    TMP_Text M_Day1;

    [SerializeField]
    TMP_Text m_Value1;

    [SerializeField]
    Transform m_Bar1;

    [SerializeField]
    TMP_Text M_Day2;

    [SerializeField]
    TMP_Text m_Value2;

    [SerializeField]
    Transform m_Bar2;

    [SerializeField]
    TMP_Text M_Day3;

    [SerializeField]
    TMP_Text m_Value3;

    [SerializeField]
    Transform m_Bar3;

    [SerializeField]
    TMP_Text M_Day4;

    [SerializeField]
    TMP_Text m_Value4;

    [SerializeField]
    Transform m_Bar4;

    [SerializeField]
    TMP_Text M_Day5;

    [SerializeField]
    TMP_Text m_Value5;

    [SerializeField]
    Transform m_Bar5;

    [SerializeField]
    DataUIDisplay.Day m_dayname1;

    public DataUIDisplay.Day dayname1
    {
        get => m_dayname1;
        set => m_dayname1 = value;
    }

    [SerializeField]
    DataUIDisplay.Day m_dayname2;

    public DataUIDisplay.Day dayname2
    {
        get => m_dayname2;
        set => m_dayname2 = value;
    }

    [SerializeField]
    DataUIDisplay.Day m_dayname3;

    public DataUIDisplay.Day dayname3
    {
        get => m_dayname3;
        set => m_dayname3 = value;
    }

    [SerializeField]
    DataUIDisplay.Day m_dayname4;

    public DataUIDisplay.Day dayname4
    {
        get => m_dayname4;
        set => m_dayname4 = value;
    }

    [SerializeField]
    DataUIDisplay.Day m_dayname5;

    public DataUIDisplay.Day dayname5
    {
        get => m_dayname5;
        set => m_dayname5 = value;
    }

    int? m_PositiveValues1;
    int? m_PositiveValues2;
    int? m_PositiveValues3;
    int? m_PositiveValues4;
    int? m_PositiveValues5;
    int?[] m_CurrentValue;

    bool m_DataChanged;
    bool m_DataSet;
    const int k_ValueMod = 5;

    DataUIDisplay.Day m_SetDayname1;
    DataUIDisplay.Day m_SetDayname2;
    DataUIDisplay.Day m_SetDayname3;
    DataUIDisplay.Day m_SetDayname4;
    DataUIDisplay.Day m_SetDayname5;

    void Start()
    {
       
        m_PositiveValues1 = M_Data.GetNo2(m_dayname1);
        m_PositiveValues2 = (M_Data.GetNo2(m_dayname2));
        m_PositiveValues3 = (M_Data.GetNo2(m_dayname3));
        m_PositiveValues4 = (M_Data.GetNo2(m_dayname4));
        m_PositiveValues5 = (M_Data.GetNo2(m_dayname5));

        m_SetDayname1 = m_dayname1;
        m_SetDayname2 = m_dayname2;
        m_SetDayname3 = m_dayname3;
        m_SetDayname4 = m_dayname4;
        m_SetDayname5 = m_dayname5;

        m_CurrentValue = new[] { m_PositiveValues1, m_PositiveValues2, m_PositiveValues3, m_PositiveValues4, m_PositiveValues5 };
    }

    void Update()
    {
        if (M_Data.DataCaptured && !m_DataSet)
        {
            m_PositiveValues1 = M_Data.GetNo2(m_dayname1);
            m_PositiveValues2 = (M_Data.GetNo2(m_dayname2));
            m_PositiveValues3 = (M_Data.GetNo2(m_dayname3));
            m_PositiveValues4 = (M_Data.GetNo2(m_dayname4));
            m_PositiveValues5 = (M_Data.GetNo2(m_dayname5));

            m_CurrentValue = new[] { m_PositiveValues1, m_PositiveValues2, m_PositiveValues3, m_PositiveValues4, m_PositiveValues5 };
            m_DataSet = true;
        }
        

        M_Day1.text = m_dayname1.ToString();
        M_Day2.text = m_dayname2.ToString();
        M_Day3.text = m_dayname3.ToString();
        M_Day4.text = m_dayname4.ToString();
        M_Day5.text = m_dayname5.ToString();

        m_Value1.text = m_CurrentValue[0].ToString();
        m_Value2.text = m_CurrentValue[1].ToString();
        m_Value3.text = m_CurrentValue[2].ToString();
        m_Value4.text = m_CurrentValue[3].ToString();
        m_Value5.text = m_CurrentValue[4].ToString();

        if (m_DataSet)
        {
            m_Bar1.transform.localScale = new Vector3(1, GetNormalizedValue(m_CurrentValue[0]), 1);
            m_Bar2.transform.localScale = new Vector3(1, GetNormalizedValue(m_CurrentValue[1]), 1);
            m_Bar3.transform.localScale = new Vector3(1, GetNormalizedValue(m_CurrentValue[2]), 1);
            m_Bar4.transform.localScale = new Vector3(1, GetNormalizedValue(m_CurrentValue[3]), 1);
            m_Bar5.transform.localScale = new Vector3(1, GetNormalizedValue(m_CurrentValue[4]), 1); 
        }

        if (m_dayname1 != m_SetDayname1 ||
            m_dayname2 != m_SetDayname2 ||
            m_dayname3 != m_SetDayname3 ||
            m_dayname4 != m_SetDayname4 ||
            m_dayname5 != m_SetDayname5)
        {
            m_SetDayname1 = m_dayname1;
            m_SetDayname2 = m_dayname2;
            m_SetDayname3 = m_dayname3;
            m_SetDayname4 = m_dayname4;
            m_SetDayname5 = m_dayname5;
            m_DataSet = false;
        }

    }

    float GetNormalizedValue(int? value)
    {
        float retVal = ((float)value - (float)GetLowestValue()) / ((float)GetHighestValue() - (float)GetLowestValue());
        return (retVal + 0.25f) * k_ValueMod;
    }

    int? GetHighestValue()
    {
        int? highestValue = -1;

        for (int i = 0; i < m_CurrentValue.Length; i++)
        {
            if (m_CurrentValue[i] > highestValue)
            {
                highestValue = m_CurrentValue[i];
            }
        }

        return highestValue;
    }

    int? GetLowestValue()
    {
        int? lowestValue = Int32.MaxValue;

        for (int i = 0; i < m_CurrentValue.Length; i++)
        {
            if (m_CurrentValue[i] < lowestValue)
            {
                lowestValue = m_CurrentValue[i];
            }
        }

        return lowestValue;
    }
}
