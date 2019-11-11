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
    public GameObject player;
    private PlayerInventory m_PlayerInventory;
    private bool m_SubmenuOpen = false;
    private GameObject m_SelectedSlot;

    private void Start()
    {
        itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        m_PlayerInventory = player.GetComponent<PlayerInventory>();
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
        if (m_SubmenuOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseItemSlotSubmenu();
            }
        }
    }

    private void OnEnable()
    {
        RefreshUI();
        EventSystem.current.SetSelectedGameObject(itemSlots[0].gameObject);
        // bug: https://answers.unity.com/questions/1096174/eventsystemsetselectedgameobject-does-not-highligh.html
        itemSlots[0].gameObject.GetComponent<Button>().OnSelect(null);
    }

    private void OnDisable()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void RefreshUI()
    {
        if (m_PlayerInventory == null) return;

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
        RefreshUI(); // para que se reordenen los items en el inventory

        CloseItemSlotSubmenu();
    }
}
