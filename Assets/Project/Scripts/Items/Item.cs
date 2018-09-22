using UnityEngine;

public class Item : MonoBehaviour {

    public ItemTemplate itemTemplate;// Привязывает основной scriptable object к физическому объекту (на который можно повесить скрипт)

    [HideInInspector]
    public bool isMouseOnItem = false;

    private void OnMouseEnter() // Если курсор мыши на предмете, то isMouseOnItem = true. Используется в скрипте ItemManager для контроля наведения курсора на айтем
    {
        isMouseOnItem = true;
    }

    private void OnMouseExit() // Если курсор мыши вышел за границы предмета, то isMouseOnItem = false
    {
        isMouseOnItem = false;
    }

}
