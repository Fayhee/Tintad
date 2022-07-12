using System;
using System.Collections;
using System.Collections.Generic;
using NFTstorage;
using TMPro;
using UnityEngine;

public class NFTDisplayInfo : MonoBehaviour
{
    [SerializeField]
     NFTData M_Data;

    [SerializeField]
    TMP_Text FMText;


    const string k_DefaultText = "NFT Title: "; 
    

    

    void Update()
    {
        if (M_Data.DataCaptured)
        {
            FMText.text = M_Data.GetName().ToString();

            List<NFTstorage.ERC721.Attribute> nAttributes = new List<NFTstorage.ERC721.Attribute>();
            nAttributes = M_Data.GetAttributes();

            string trait = nAttributes[0].trait_type;
            string value = nAttributes[0].value.ToString();

            Debug.Log("trait: " + trait);
            Debug.Log("value: " + value);

            
        }

        

    }
    
}
