using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of EquipmentManager found");
            return;
        }

        instance = this;
    }

    #endregion

    #region Delegate OnItemEquipped()
    public delegate void OnItemEquipped(Equipment newItem, Equipment oldItem);
    public OnItemEquipped onItemEquippedCallback;
    #endregion 


    Equipment[] currentEquipment;
    InventorySystem inventory;
    

    void Start ()
    {
        inventory = InventorySystem.instance;
        int numberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numberOfSlots];
	}
	
	
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
	}

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int) newItem.equipmentSlot;
        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);
        }

        currentEquipment[slotIndex] = newItem;

        if(onItemEquippedCallback != null)
        {
            onItemEquippedCallback.Invoke(newItem, oldItem);
        }
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);
            currentEquipment[slotIndex] = null;

            if (onItemEquippedCallback != null)
            {
                onItemEquippedCallback.Invoke(null, oldItem);
            }
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    
}
