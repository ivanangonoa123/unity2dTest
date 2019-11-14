using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpotCollider : MonoBehaviour
{
    public GameObject bgIcon;
    public bool initial = false;
    private Animator m_bgIconAnim;

    private void Start()
    {
        m_bgIconAnim = bgIcon.GetComponent<Animator>();
        m_bgIconAnim.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !initial)
        {
            // @TODO https://forum.unity.com/threads/assign-camera-bounds-at-runtime.526513/
            // buscar una forma mas performante de hacer esto
            // hay que obtener la virtual camera antes de disablear el CinemachineBrain sino devuelve null
            CinemachineVirtualCamera vCam = (CinemachineVirtualCamera)Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera;

            Camera.main.GetComponent<CinemachineBrain>().enabled = false;
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

            Debug.Log(vCam);
            Debug.Log(vCam.GetComponent<CinemachineConfiner>());
            vCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = GetComponent<PolygonCollider2D>();
            vCam.GetComponent<CinemachineConfiner>().InvalidatePathCache();

            Camera.main.GetComponent<CinemachineBrain>().enabled = true;

            m_bgIconAnim.SetBool("isActive", true); // glow en el mapa
            return;
        }

        initial = false;
        return;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_bgIconAnim.SetBool("isActive", false);
        }
    }
}
