using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public string SpawnPosition;
    private bool m_ShowingText = false;

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
                Time.timeScale = 1;
                mainText.GetComponent<Text>().text = "";
            }
        }
    }

    // Move to instanciated gameobject
    public void SetMainText(string text)
    {
        GameObject mainText = GameObject.FindGameObjectWithTag("MainText");
        if (mainText)
        {
            Time.timeScale = 0;
            mainText.GetComponent<Text>().text = text;
            m_ShowingText = true;
        }
    }
}
