using UnityEngine;

public class GameSystem : MonoBehaviour {

    public Texture2D cursorWithoutWeapon;
    public Texture2D cursorWithWeapon;
    public InventoryUI inventoryUI;

    private InventorySystem inventory;

	private void Start () {
        inventory = InventorySystem.instance;
        Cursor.SetCursor(cursorWithoutWeapon, new Vector2(16, 16), CursorMode.Auto);
	}

    private void Update()
    {
        if(inventoryUI.inventoryWindow.activeSelf || inventory.currentWeaponIndex == 0)
        {
            Cursor.SetCursor(cursorWithoutWeapon, new Vector2(16, 16), CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(cursorWithWeapon, new Vector2(16, 16), CursorMode.Auto);
        }
    }
}
