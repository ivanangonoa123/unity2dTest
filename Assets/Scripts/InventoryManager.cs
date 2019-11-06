using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventory;

    void Start()
    {
        // desactivamos el inventory si esta activado por casualidad
        if (inventory != null)
        {
            inventory.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && inventory != null)
        {
            Animator animator = inventory.GetComponent<Animator>();

            if (animator != null)
            {
                if (!inventory.activeSelf)
                {
                    Time.timeScale = 0; // pausa
                    // activo el inventory antes para que corra la
                    // animacion y activo la animacion
                    inventory.SetActive(true);
                    animator.updateMode = AnimatorUpdateMode.UnscaledTime;
                    animator.SetBool("open", true);
                } else
                {
                    StartCoroutine(CloseInventory(animator));
                }
            }
        }
    }

    IEnumerator CloseInventory(Animator animator)
    {
        // arranca la animacion para cerrar
        animator.SetBool("open", false);
        // espera la duracion de la transicion que esta sucediendo
        // animator.GetCurrentAnimatorStateInfo(0)
        // en tiempo real (WaitForSeconds comun no funca
        // porque timeScale esta en 0 todavia
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSecondsRealtime(animator.GetCurrentAnimatorStateInfo(0).length);
        Debug.Log("ue2");
        // desactiva el inventory (no estoy seguro si realmente es necesario desactivarlo
        inventory.SetActive(false);
        Time.timeScale = 1.0f; // ahora si despauso
    }
}
