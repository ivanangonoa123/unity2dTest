using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string text;
    private bool m_IsColliding = false;

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
            if (text != null)
            {
                GameManager.Instance.SetMainText(text);
            }
        }
    }
}
