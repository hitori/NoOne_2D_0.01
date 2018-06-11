using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InventorySlot : MonoBehaviour {

    ItemTemplate item;
    public Image itemIcon;
    public GameObject rightClickMenu;
    public RectTransform inventorySlot;
    


    // Добавляем айтем в слот инвентаря (не путать с физическим добавлением предмета в лист айтемов)
    public void AddItemToSlot(ItemTemplate newItem)
    {
        item = newItem;

        itemIcon.sprite = item.icon;
        itemIcon.enabled = true;
    }

    // Очищаем слот от айтема, удаляем иконку
    public void ClearItemFromSlot()
    {
        item = null;

        itemIcon.sprite = null;
        itemIcon.enabled = false;
    }

    // Функция использования предмета. Ссылается на базовую функцию из ItemTemplate
    public void LeftButtonClick()
    {
        rightClickMenu.SetActive(false);
        if (item != null)
        {
            item.LeftButtonClickMain();
            rightClickMenu.SetActive(false);
        }
    }

    public void RightButtonClick()
    {
        rightClickMenu.SetActive(false);
        if (item != null)
        {
            rightClickMenu.SetActive(true);
            rightClickMenu.transform.position = inventorySlot.position;
        }
        else
            rightClickMenu.SetActive(false);
    }


    public void BreakItem()
    {
        item.BreakItemMain();
        rightClickMenu.SetActive(false);
    }

    public void ViewItem()
    {
        item.ViewItemMain();
        rightClickMenu.SetActive(false);
    }

    public void DropItem()
    {
        item.DropItemMain();
        rightClickMenu.SetActive(false);
    }
}
