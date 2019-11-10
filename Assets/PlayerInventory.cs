using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> items;
    [SerializeField] int itemsLimit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool AddItem(Item item)
    {
        if (IsFull())
            return false;

        items.Add(item);
        return true;
    }

    public bool RemoveItem(Item item)
    {
        if (items.Remove(item))
        {
            return true;
        }
        return false;
    }

    public bool IsFull()
    {
        return items.Count >= itemsLimit;
    }
}
