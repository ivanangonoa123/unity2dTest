using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotCollider : MonoBehaviour
{
    public GameObject bgIcon;
    private Animator m_bgIconAnim;

    private void Start()
    {
        m_bgIconAnim = bgIcon.GetComponent<Animator>();
        m_bgIconAnim.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
            m_bgIconAnim.SetBool("isActive", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_bgIconAnim.SetBool("isActive", false);
        }
    }
}
