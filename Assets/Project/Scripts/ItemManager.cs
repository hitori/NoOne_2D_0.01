using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemManager : MonoBehaviour {

    #region Singleton

    public static ItemManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance found");
            return;
        }

        instance = this;
    }


    #endregion


    public Transform player;
    float playerRadius = 2f;
    public Text descriptionTextField;
    public Text dialogCloud;
    float timer;
    float timerStartValue = 2f;
    Item item;

    private void Start()
    {
        descriptionTextField.text = "";
        dialogCloud.text = "";
        timer = timerStartValue;
        
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 playerPositionOnScreen = Camera.main.WorldToScreenPoint(player.position + Vector3.up * 2.5f);
        dialogCloud.transform.position = Vector3.Lerp(dialogCloud.transform.position, playerPositionOnScreen, Time.deltaTime * 20f);

        
        timer -= Time.deltaTime;
        if (timer < 0)
            timer = 0;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Item"))
            {
                item = hit.collider.gameObject.GetComponent<Item>();

                if (item.isMouseOnItem)
                {
                    descriptionTextField.text = item.itemTemplate.description;
                    

                    if (Input.GetMouseButtonDown(0))
                    {
                        float distanceToItem = Vector3.Distance(hit.point, player.position + Vector3.up);
                        

                        if (distanceToItem <= playerRadius)
                        {
                            
                            if (InventorySystem.instance.AddItem(item.itemTemplate))
                            {
                                dialogCloud.text = "Picked " + item.itemTemplate.name;
                                Destroy(hit.collider.gameObject);
                            }

                            ResetTimer();
                        }

                        else
                        {
                            int randomInt = Random.Range(0, 5);
                            switch (randomInt)
                            {
                                case 0:
                                    dialogCloud.text = "This item is too far away";
                                    break;
                                case 1:
                                    dialogCloud.text = "Can't reach!";
                                    break;
                                case 2:
                                    dialogCloud.text = "I need to get closer";
                                    break;
                                case 3:
                                    dialogCloud.text = "My hands are to short";
                                    break;
                                case 4:
                                    dialogCloud.text = "Nope";
                                    break;
                            }
                            ResetTimer();
                        }
                            
                    }
                }

                
            }

            else if (descriptionTextField.text != "")
            {
                descriptionTextField.text = "";
            }                
        }

        if (timer <= 0 && dialogCloud.text != "")
        {
            Invoke("ClearingDialogCloud", 0f);
        }
    }

    void ClearingDialogCloud()
    {
        dialogCloud.text = "";
    }

    void ResetTimer()
    {
        timer = timerStartValue;
    }

    void PickingUpItem ()
    {

    }







    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position + Vector3.up, playerRadius);
    }


}
