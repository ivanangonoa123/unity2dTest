using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public string SpawnPosition;
    private bool m_ShowingText = false;
    private bool m_showTextCooldown = true; // cooldown

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
    }

    private void Update()
    {
        // @TODO move to textManager
        if (m_ShowingText)
        {
            GameObject mainText = GameObject.FindGameObjectWithTag("MainText");

            if (Input.GetKeyDown(KeyCode.E))
            {
                m_ShowingText = false;
                mainText.GetComponent<MainTextWriter>().clearText();
                Time.timeScale = 1;
                StartCoroutine(setTextCoolDown());
            }
        }
    }

    // Move to instanciated gameobject
    public void SetMainText(string text)
    {
        GameObject mainText = GameObject.FindGameObjectWithTag("MainText");
        if (mainText && m_showTextCooldown)
        {
            Time.timeScale = 0;
            mainText.GetComponent<MainTextWriter>().setText(text);
            m_ShowingText = true;
        }
    }

    private IEnumerator setTextCoolDown()
    {
        m_showTextCooldown = false;
        yield return new WaitForSeconds(1f);
        m_showTextCooldown = true;
    }
}
