using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item item;
    private bool m_IsColliding = false;
    private string m_Text = "You picked ";

    private void OnValidate()
    {
        if (item)
        {
            GetComponent<SpriteRenderer>().sprite = item.pickUpSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_IsColliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_IsColliding = false;
    }

    private void Update()
    {
        if (m_IsColliding && Input.GetKeyUp(KeyCode.E))
        {
            if (GameManager.Instance.AddInventoryItem(item))
            {
                GameManager.Instance.SetMainText(m_Text + "<color=green>" + item.itemName + "</color>");
                Destroy(gameObject);
            }
        }
    }
}
