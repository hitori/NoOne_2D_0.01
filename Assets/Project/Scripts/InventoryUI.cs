using UnityEngine;

public class InventoryUI : MonoBehaviour {

    InventorySystem inventory;
    public GameObject inventoryWindow; // Окно инвентаря
    public Transform itemParent;
    InventorySlot[] slots;

    void Start ()
    {
        inventory = InventorySystem.instance; // Кэшируем инстанс InventorySystem для оптимальной работы
        inventory.onItemStatusChangedCallback += UpdateUI; // Добавляем к текущему колбэку выполнение метода UpdateUI

        slots = itemParent.GetComponentsInChildren<InventorySlot>(); // Создаем список слотов. Сколько объектов вложено в itemParent
    }
	

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I)) // Проверяем, была ли нажата клавиша открытия инвентаря
        {
            inventoryWindow.SetActive(!inventoryWindow.activeSelf); // Меняет статус активности на противоположный
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++) // Проходит по всем объектам, вложенным в itemParent
        {
            if (i < inventory.items.Count) // Если номер текущего айтема не превышает количество записей в листе айтемов (из InventorySystem)
            {
                slots[i].AddItemToSlot(inventory.items[i]); // Добавляем графическое изображение айтема в окно слота
            }

            else
            {
                slots[i].ClearItemFromSlot(); // для слотов, номера которых больше, чем количество записей в листе айтемов, ячейки слотов очищаются 
            }
        }
    }
}
