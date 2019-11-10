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
    public GameObject player;
    private PlayerInventory m_playerInventory;

    private void Start()
    {
        itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        m_playerInventory = player.GetComponent<PlayerInventory>();
    }

    //private void OnValidate()
    //{
    //    // @TODO create item slots with player inventory limit
    //    if (itemsParent != null)
    //       itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();

    //    RefreshUI();
    //}

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
        if (m_playerInventory == null) return;

        int i = 0;

        for (; i < m_playerInventory.items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = m_playerInventory.items[i];
        }

        // si habia dos items i va a ser 1 aca, el resto de items
        // van a ser null
        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }
}
