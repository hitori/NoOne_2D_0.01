using UnityEngine;

[CreateAssetMenu (fileName = "Pickable Item", menuName = "Items/Create pickable item")]
public class ItemTemplate : ScriptableObject {

    public new string name;
    public string description;
    public Sprite icon = null;
}
