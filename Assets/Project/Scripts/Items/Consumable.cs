using UnityEngine;

[CreateAssetMenu(fileName = "Consumable Item", menuName = "Items/Consumable/Create a consumable item")]
public class Consumable : ItemTemplate {

    public override void Use()
    {
        base.Use();
        Debug.Log("Eating " + this.name);
    }
}
