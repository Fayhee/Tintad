using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour, IInventoryItem
{
   public string Name
    {
        get
        {
            return "Food Waste";
        }
    }

    public Sprite _Image = null;

    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }

    public void OnDrop()
    {
        RaycastHit hit = new RaycastHit();
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        

        if (Physics.Raycast(ray, out hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
            //gameObject.transform.position = new Vector3(0,0,1.69f);
            Debug.Log("Item Droped");
        }
    }
}

