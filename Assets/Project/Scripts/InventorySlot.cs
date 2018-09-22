using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour {

    ItemTemplate item;
    public Image itemIcon;
    public GameObject rightClickMenu; // Все слоты ссылаются на один объект. Возможно стоит это поправить
                                      //public RectTransform inventorySlot; // Для определения местоположения текущего слота и размещения в нем меню rightClick

    /*
    private void Update() //если убираем курсор со слота, то rightClickMenu исчезает. Пока не работает
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log(EventSystem.current.transform.name);
            rightClickMenu.SetActive(false);
        }

    }
    */

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
            item.Use();
            rightClickMenu.SetActive(false);
        }
    }

    public void RightButtonClick()
    {
        rightClickMenu.SetActive(false);
        if (item != null)
        {
            rightClickMenu.SetActive(true);
            rightClickMenu.transform.position = this.transform.position;
        }
        else
            rightClickMenu.SetActive(false);
    }


    public void BreakItem()
    {
        item.BreakItemMain();// не нужен ли override?
        rightClickMenu.SetActive(false);
    }

    public void ViewItem()
    {
        item.ViewItemMain();// не нужен ли override?
        rightClickMenu.SetActive(false);
    }

    public void DropItem()
    {
        item.DropItemMain();// не нужен ли override?
        rightClickMenu.SetActive(false);
    }
}
