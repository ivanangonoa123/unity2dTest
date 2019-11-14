using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    /* 
     * tutorial de inventory: https://www.youtube.com/watch?v=bTPEMt1RG3s
     * 
     */
    public Transform itemsParent;
    public ItemSlot[] itemSlots;
    public GameObject itemSlotSubmenu;
    public GameObject checkMenu;
    public GameObject player;

    private PlayerInventory m_PlayerInventory;
    private GameObject m_SelectedSlot;
    private bool m_SubmenuOpen = false;
    private bool m_CheckMenuOpen = false;

    private void Awake()
    {
        Debug.Log("awake");
        itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        m_PlayerInventory = player.GetComponent<PlayerInventory>();
        Debug.Log("m_PlayerInventory " + m_PlayerInventory);
    }

    //private void OnValidate()
    //{
    //    // @TODO create item slots with player inventory limit
    //    if (itemsParent != null)
    //       itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();

    //    RefreshUI();
    //}

    private void Update()
    {
        if (m_CheckMenuOpen)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Escape))
            {
                CloseCheckMenu();
            }
        } else if (m_SubmenuOpen) {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseItemSlotSubmenu();
            }
        }
    }

    private void OnEnable()
    {
        // @TODO bug sacar esto de OnEnable
        // RefreshUI esta como coroutine para que aguarde un cacho hasta que los children itemSlots se activen
        StartCoroutine(RefreshUI());

        EventSystem.current.SetSelectedGameObject(itemSlots[0].gameObject); // marcar el primer item del inv
        // @TODO bug: https://answers.unity.com/questions/1096174/eventsystemsetselectedgameobject-does-not-highligh.html
        itemSlots[0].gameObject.GetComponent<Button>().OnSelect(null);
    }

    private void OnDisable()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    private IEnumerator RefreshUI()
    {
        yield return null;

        if (m_PlayerInventory == null) yield break;

        int i = 0;

        for (; i < m_PlayerInventory.items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = m_PlayerInventory.items[i];
        }

        // si habia dos items i va a ser 1 aca, el resto de items
        // van a ser null
        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }

    public void OpenCheckMenu()
    {
        m_CheckMenuOpen = true;
        checkMenu.SetActive(true);

        Item selectedItem = m_SelectedSlot.GetComponent<ItemSlot>().Item;

        Debug.Log(checkMenu.transform.Find("Title"));
        Debug.Log(selectedItem.description);

        checkMenu.transform.Find("Title").GetComponent<TMPro.TextMeshProUGUI>().text = selectedItem.name;
        checkMenu.transform.Find("Image").GetComponent<Image>().sprite = selectedItem.checkSprite;
        checkMenu.transform.Find("Desc").GetComponent<MainTextWriter>().setText(selectedItem.description);
    }

    public void CloseCheckMenu()
    {
        checkMenu.SetActive(false);
        m_CheckMenuOpen = false;
    }

    public void OpenItemSlotSubmenu()
    {
        // buscamos el slot actualmente seleccionado
        m_SelectedSlot = EventSystem.current.currentSelectedGameObject;

        if (m_SelectedSlot.GetComponent<ItemSlot>().Item != null) { 
            itemSlotSubmenu.SetActive(true); // activamos el itemSlotSubmenu
            itemSlotSubmenu.transform.position = m_SelectedSlot.transform.position; // lo posicionamos
            EventSystem.current.SetSelectedGameObject(itemSlotSubmenu.transform.GetChild(0).gameObject); // le damos focus
            m_SubmenuOpen = true; // seteamos el bool para chequear por el cierre
        }
    }

    private void CloseItemSlotSubmenu()
    {
        itemSlotSubmenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(m_SelectedSlot);
        m_SelectedSlot = null;
        m_SubmenuOpen = false;
    }

    public void UseItem()
    {
        Item selectedItem = m_SelectedSlot.GetComponent<ItemSlot>().Item; 
        selectedItem.Use();

        m_PlayerInventory.RemoveItem(selectedItem); // borramos item del player
        StartCoroutine(RefreshUI()); // para que se reordenen los items en el inventory

        CloseItemSlotSubmenu();
    }
}
