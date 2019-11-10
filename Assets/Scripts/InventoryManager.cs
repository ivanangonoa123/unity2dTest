using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventory;
    public GameObject map;

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
            openMenu(inventory);
        } else if (Input.GetKeyDown(KeyCode.M) && map != null)
        {
            openMenu(map);
        }
    }

    void openMenu(GameObject menu)
    {
        Animator animator = menu.GetComponent<Animator>();

        if (animator != null)
        {
            if (!menu.activeSelf)
            {
                Time.timeScale = 0; // pausa
                // activo el menu antes para que corra la
                // animacion y activo la animacion
                menu.SetActive(true);
                animator.updateMode = AnimatorUpdateMode.UnscaledTime;
                animator.SetBool("open", true);
            }
            else
            {
                StartCoroutine(CloseInventory(menu));
            }
        }
    }

    IEnumerator CloseInventory(GameObject menu)
    {
        Animator animator = menu.GetComponent<Animator>();
        // arranca la animacion para cerrar
        animator.SetBool("open", false);
        // espera la duracion de la transicion que esta sucediendo
        // animator.GetCurrentAnimatorStateInfo(0)
        // en tiempo real (WaitForSeconds comun no funca
        // porque timeScale esta en 0 todavia
        yield return new WaitForSecondsRealtime(animator.GetCurrentAnimatorStateInfo(0).length);
        // desactiva el inventory (no estoy seguro si realmente es necesario desactivarlo
        inventory.SetActive(false);
        Time.timeScale = 1.0f; // ahora si despauso
    }
}
