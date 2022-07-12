using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioFuel : MonoBehaviour
{
    public GameObject trash;
    public GameObject wood;
    public GameObject weed;

    private bool trashIsActive;
    private bool woodIsActive;
    private bool weedIsActive;
    // Start is called before the first frame update
    void Start()
    {
        trashIsActive = trash.activeSelf;
        woodIsActive = wood.activeSelf;
        weedIsActive = weed.activeSelf;
        Debug.Log(trash.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        if ((trash.activeSelf == false) && (wood.activeSelf == false) && (weed.activeSelf == false))
        {
            Debug.Log("BioFuel");
        }

        Debug.Log(trash.activeSelf);
    }
}
