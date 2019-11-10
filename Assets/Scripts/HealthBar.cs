using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform m_bar;

    private void Start()
    {
        Transform bar = transform.Find("Bar");
        bar.localScale = new Vector3(.4f, 1f);
    }

    private void Update()
    {
        
    }

    private void SetSize(float sizeNormalized)
    {
        m_bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}
