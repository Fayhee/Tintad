using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchTextType : MonoBehaviour
{
    public string currentProductSearch;
    public Text searchText;
    public Button searchButton;

    void Start()
    {
        searchButton.interactable = false;
        searchText.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    
    {
        yield return new WaitForSeconds(1);
        
        foreach (char letter in currentProductSearch)
        {
            searchText.text += letter;
            yield return new WaitForSeconds(0.1f); ; 
        }

        searchButton.interactable = true;
    }
}
