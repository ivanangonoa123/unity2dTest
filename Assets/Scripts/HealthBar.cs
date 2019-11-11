using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform m_bar;

    private void Awake()
    {
        m_bar = transform.Find("Bar");
    }

    public void SetSize(float sizeNormalized)
    {
        m_bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}
