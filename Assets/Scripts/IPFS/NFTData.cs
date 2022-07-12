using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using NFTstorage;
using NFTstorage.ERC721;
using Object = System.Object;
using Random = UnityEngine.Random;

public class NFTData : MonoBehaviour
{
    string path;
    
    TheData m_Data;
    bool m_DataCaptured;

    public bool DataCaptured
    {
        get => m_DataCaptured;
    }

    void OnEnable()
    {
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        path = Helper.GenerateGatewayPath("https://" + "bafkreigbkm7p5gim36kwmpevtl3ceo6iq27hkg2jygyo6dbr4wwt66xm5m",
               Constants.GatewaysSubdomain[0], true);
        string url = path;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error occured with web request");
            }
            else
            {
                m_Data = JsonConvert.DeserializeObject<TheData>(webRequest.downloadHandler.text);
                m_DataCaptured = true;
                Debug.Log("Data captured successfully");
            }
        }
    }

    public string GetName()
    {
        if (m_DataCaptured)
        {
            return m_Data.name;
        }
        else
        {
            return "Nothing";
        }
    }

    public string GetDescription()
    {
        if (m_DataCaptured)
        {
            return m_Data.description;
        }
        else
        {
            return "Nothing";
        }
    }

    public string GetImage()
    {
        if (m_DataCaptured)
        {
            return m_Data.external_url;
        }
        else
        {
            return "Nothing";
        }
    }




    public List<NFTstorage.ERC721.Attribute> GetAttributes()
    {
        if (m_DataCaptured)
        {
            return m_Data.attributes;
        }
        else
        {
            return null;
        }
    }
    
}



public class TheData
{    
public string description;
public string external_url;
public string name;
public List<NFTstorage.ERC721.Attribute> attributes;

}
