using UnityEngine;
using UnityEngine.UI;

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

    #region Delegate OnItemPickedUp()
    public delegate void OnItemPickedUp();
    public OnItemPickedUp onItemPickedUpCallback;
    #endregion


    public Transform player;
    float playerRadius = 2f;
    public Text descriptionTextField;
    public Text dialogCloud;
    float timer;
    float timerStartValue = 2f;
    Item item;
    int layerMask;

    private void Start()
    {
        descriptionTextField.text = "";
        dialogCloud.text = "";
        timer = timerStartValue;
        layerMask = 1 << LayerMask.NameToLayer("Item");
    }

    private void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Пускает луч из камеры в точку нахождения мыши (!!если камера будет перспективной, то обзательно доработать это место!!)
        RaycastHit hit;
        Vector3 playerPositionOnScreen = Camera.main.WorldToScreenPoint(player.position + Vector3.up * 2.5f);// Вычисляет позицию игрока в координатах экрана
        dialogCloud.transform.position = Vector3.Lerp(dialogCloud.transform.position, playerPositionOnScreen, Time.deltaTime * 20f);// Прикрепляет облако диалога к позиции Игрока

        
        timer -= Time.deltaTime; // Таймер ведет отсчет от стартового значения до нуля
        if (timer < 0)
            timer = 0;

       
        if (!GloabalVars.isInAttackState_g) // Если игрок в данный момент не целится из оружия
        { 
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) // Если луч попал в какой-либо коллайдер на уровне маски "Item"
            {
                if (hit.collider.CompareTag("Item")) // Если луч попал в коллайдер с тэгом Item
                {

                    item = hit.collider.gameObject.GetComponent<Item>(); // кэшируем айтем, в который попали

                    if (item.isMouseOnItem) // идет ссылка на скрипт Item. Правда если мышь наведена на айтем
                    {
                        descriptionTextField.text = item.itemTemplate.description; // Поле описания заполняется информацией из параметров конкретного айтема

                        if (Input.GetMouseButtonDown(0))
                        {
                            float distanceToItem = Vector3.Distance(hit.point, player.position + Vector3.up); // Расчитывает расстояние от Игрока до указателя мышки
                        

                            if (distanceToItem <= playerRadius) // Если расстояние до айтема меньше или равно радиусу взаимодействия Игрока
                            {
                            
                                if (InventorySystem.instance.AddItem(item.itemTemplate)) // Добавляет предмет в инвентарь (не путать с ГУИ)
                                {
                                    dialogCloud.text = "Picked " + item.itemTemplate.name;
                                    Destroy(hit.collider.gameObject);

                                    if (onItemPickedUpCallback != null)
                                    {
                                        onItemPickedUpCallback.Invoke();
                                    }
                                }

                                ResetTimer(); // Обновляет таймер на стартовое значение
                            }

                            else
                            {
                                int randomInt = Random.Range(0, 5); // Генерирует рандомное число для разных вариантов диалога
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
                                ResetTimer(); // Обновляет таймер на стартовое значение
                            }
                            
                        }
                    }

                
                }

                else if (timer-1 <= 0 && descriptionTextField.text != "") // Если таймер <= 0 и курсор уходит с предмета, то вызывается функция ClearingDescriptionText() и окно описания очищается
                {
                    Invoke("ClearingDescriptionText", 0f);
                }
            }

            else if (timer - 1 <= 0 && descriptionTextField.text != "") // Если таймер <= 0 и курсор уходит с предмета, то вызывается функция ClearingDescriptionText() и окно описания очищается
            {
                Invoke("ClearingDescriptionText", 0f);
            }

            if (timer <= 0 && dialogCloud.text != "") // Если таймер <= 0 и диалоговое окно не пустое, то вызывается функция ClearingDialogCloud() и окно диалога очищается
            {
                Invoke("ClearingDialogCloud", 0f);
            }
        }

    }

    // Очищает текст в окне описания
    void ClearingDescriptionText()
    {
        descriptionTextField.text = "";
    }

    // Очищает текст в окне диалога
    void ClearingDialogCloud()
    {
        dialogCloud.text = "";
    }


    // Обновляет таймер на стартовое значение
    void ResetTimer()
    {
        timer = timerStartValue;
    }


    // Показывает радиус взаимодействия вокруг игрока, на каком расстоянии он может брать предметы
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position + Vector3.up, playerRadius);
    }


}
