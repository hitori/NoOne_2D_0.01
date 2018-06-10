using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InventorySlot : MonoBehaviour {

    ItemTemplate item;
    public Image itemIcon;

    


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
    public void UseItem()
    {
        if (item != null)
        {
            item.UseItemMain();
        }
    }

    public void RightButtonClick()
    {
        if (item != null)
        {
            item.RightButtonClickMain();
        }
    }
}
