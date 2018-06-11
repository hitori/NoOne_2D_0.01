using UnityEngine;

[CreateAssetMenu (fileName = "Pickable Item", menuName = "Items/Create pickable item")]
public class ItemTemplate : ScriptableObject {

    public new string name;
    public string description;
    public Sprite icon = null;
    public enum itemClass {consumable, equipment, ammo, keys, trading};
    public itemClass currentClass;
    public bool crafting = false;



    public virtual void LeftButtonClickMain()
    {

        if (currentClass == itemClass.consumable)
        {
            Debug.Log("Eating " + name);
        }

        else if (currentClass == itemClass.equipment)
        {
            Debug.Log("Equipped " + name);
        }

        else if (currentClass == itemClass.ammo)
        {
            Debug.Log("Reloaded with " + name);
        }

        else if (currentClass == itemClass.keys)
        {
            Debug.Log("Used key " + name);
        }

        else if (currentClass == itemClass.trading)
        {
            Debug.Log("Trading " + name);
        }
    }

    public virtual void RightButtonClickMain()
    {
        Debug.Log("Right click use of " + name);
    }

    public virtual void BreakItemMain()
    {
        Debug.Log("What's inside " + name + " I wonder");
    }

    public virtual void ViewItemMain()
    {
        Debug.Log("Let's see " + name);
    }

    public virtual void DropItemMain()
    {
        Debug.Log("Dropped " + name);
    }
}
