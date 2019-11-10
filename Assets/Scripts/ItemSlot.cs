using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image m_image;

    public Item _item;
    public Item Item {
        get { return _item; }
        set {
            _item = value;

            if (_item == null)
            {
                m_image.enabled = false;
            } else {
                m_image.sprite = _item.icon;
                m_image.enabled = true;
            }
        }
    }
    
    private void OnValidate()
    {
        if (m_image == null)
        {
            m_image = GetComponent<Image>();
        }
    }
}
