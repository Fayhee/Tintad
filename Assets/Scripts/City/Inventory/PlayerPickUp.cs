using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{

    public TheInventory theInventory;
    public GameObject hand;
    public HUD hUD;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private IInventoryItem itemToPickUp = null;

    // Update is called once per frame
    void Update()
    {
        if (itemToPickUp != null & Input.GetKeyDown(KeyCode.F))
        {
            theInventory.AddItem(itemToPickUp);
            itemToPickUp.OnPickup();
            hUD.CloseMessagePanel();

        }
    }

   
    private void OnTriggerEnter(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null)
        {
            itemToPickUp = item;
            hUD.OpenMessagePanel("-press F to Pickup-");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null)
        {
            hUD.CloseMessagePanel();
            itemToPickUp = null;
        }
    }
}
