using System;
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

    private void Start()
    {
        //HideMouseCursor();
    }

    private void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

    // @TODO Move to instanciated gameobject
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

    // @TODO Move to instanciated gameobject
    public bool AddInventoryItem(Item item)
    {
        PlayerInventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        // la funcion al ser bool puede devolver true/false al igual que AddItem
        // del script Inventory
        return inventory.AddItem(item);
    }

    private IEnumerator setTextCoolDown()
    {
        m_showTextCooldown = false;
        yield return new WaitForSeconds(1f);
        m_showTextCooldown = true;
    }
}
