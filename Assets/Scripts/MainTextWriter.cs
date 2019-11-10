using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTextWriter : MonoBehaviour
{
    public float delay;
    private TMPro.TextMeshProUGUI m_text;
    // Start is called before the first frame update
    void Start()
    {
        m_text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void setText(string text)
    {
        m_text.text = text;
        StartCoroutine(setTypedText(text));
    }

    public void clearText()
    {
        StopAllCoroutines();
        m_text.text = "";
    }

    private IEnumerator setTypedText(string text)
    {
        for (int i = 0; i <= text.Length; i++)
        {
            m_text.maxVisibleCharacters = i;
            yield return new WaitForSecondsRealtime(delay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
