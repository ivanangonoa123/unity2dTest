using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private Image m_image;

    public Item _item;
    public Item Item {
        get { return _item; }
        set {
            _item = value;

            if (_item == null)
            {
                m_image.enabled = false;
            } else {
                Debug.Log("m_image", m_image.sprite);
                Debug.Log("_item.icon", _item.icon);
                m_image.sprite = _item.icon;
                m_image.enabled = true;
            }
        }
    }
    
    private void Start()
    {
        if (m_image == null)
        {
            m_image = GetComponentsInChildren<Image>()[1];
        }
    }
}
