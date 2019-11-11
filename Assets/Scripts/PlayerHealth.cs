using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    // @TODO usar eventos para updatear el healthBar y no tener una referencia aca
    public GameObject healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.GetComponent<HealthBar>().SetSize(health);
    }

    public void UpdatePlayerHealth(float healthDiff)
    {
        health += healthDiff;
        
        if (health >= 1)
        {
            health = 1;
        }

        healthBar.GetComponent<HealthBar>().SetSize(health);
    }
}
