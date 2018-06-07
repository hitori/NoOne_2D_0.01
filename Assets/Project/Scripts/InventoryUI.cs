using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    InventorySystem inventory;
    public GameObject inventoryWindow;

	void Start ()
    {
        inventory = InventorySystem.instance;
        inventory.onItemStatusChangedCallback += UpdateUI;
    }
	

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryWindow.SetActive(!inventoryWindow.activeSelf);
        }

    }

    void UpdateUI()
    {
        
    }
}
