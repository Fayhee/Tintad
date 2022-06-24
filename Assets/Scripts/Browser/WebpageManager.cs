using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebpageManager : MonoBehaviour
{
    public Webpage[] webpages;
    public Text shoppingTabButtonText;
    public Text url;
    public GameObject noSSLLockIcon;
    public Button backButton;
    public Button forwardButton;
    
    private Stack<string> back;
    private Stack<string> forward;
    private string currentWebpageName;

    void Start()
    {
        currentWebpageName = "";
        back = new Stack<string>();
        forward = new Stack<string>();
    }

    public void GoToWebpage(string webpageName)
    {
        if (currentWebpageName != webpageName)
        {
            foreach (Webpage webpage in webpages)
            {
                if (webpage.name == webpageName)
                {
                    
                    webpage.webpageObject.SetActive(true);
                    
                    shoppingTabButtonText.text = webpageName;
                    url.text = webpage.url;
                    noSSLLockIcon.SetActive(!webpage.sslLock);

                    back.Push(currentWebpageName);
                    backButton.interactable = true;
                    currentWebpageName = webpageName;
                }
                else
                {
                    webpage.webpageObject.SetActive(false);
                }
            }
        }  
    }

    public void Back()
    {
        if (back.Count != 0)
        {
            if (back.Peek() != "Search")
            {
                string prevWebpage = back.Pop();
                forward.Push(currentWebpageName);
                GoToWebpage(prevWebpage);
                currentWebpageName = prevWebpage;

                if ((back.Count <= 1) || (back.Peek() == "Search"))
                {
                    backButton.interactable = false;
                }
                if (forward.Count == 1)
                {
                    forwardButton.interactable = true;
                }
            }
        }
    }

    public void Forward()
    {
        if (forward.Count != 0)
        {
            string prevWebpage = forward.Pop();
            back.Push(currentWebpageName);
            GoToWebpage(prevWebpage);
            currentWebpageName = prevWebpage;

            if (forward.Count <= 0)
            {
                forwardButton.interactable = false;
            }
            if ((back.Count == 2) && (back.Peek() != "Search"))
            {
                backButton.interactable = true;
            }
        }
    }
}
