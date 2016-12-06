using UnityEngine;
using System.Collections;

public class InventoryPanel : MonoBehaviour
{
    public GameObject player;
    InventoryController invCon;
    GameObject item = null;

	// Use this for initialization
	void Start ()
    {
        invCon = player.GetComponent<InventoryController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(invChanged())
        {
            hideItem();
            showItem();
        }
	}

    bool invChanged()
    {
        if (invCon.currItem != invCon.prevItem)
        {
            invCon.prevItem = invCon.currItem;
            return true;
        }
        return false;
    }

    void hideItem()
    {
        if(item != null)
        {
            Destroy(item.gameObject);
        }
    }

    void showItem()
    {
        if (invCon.currItem != null)
        {
            GameObject newItem = (GameObject)Instantiate(invCon.currItem, transform.position, Quaternion.identity);
            item = newItem;
            item.transform.parent = this.transform;
        }
       
    }
}
