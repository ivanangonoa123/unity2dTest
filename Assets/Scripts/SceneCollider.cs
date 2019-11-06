using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCollider : MonoBehaviour
{
    public string sceneName;
    public string nextSpawnPosition;
    BoxCollider2D m_ObjectCollider;

    // Start is called before the first frame update
    void Start()
    {
        m_ObjectCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LoadScene(sceneName);
        }
    }

    // @TODO move to scene manager
    void LoadScene(string sceneName)
    {
        GameManager.Instance.SpawnPosition = nextSpawnPosition;
        SceneManager.LoadScene(sceneName);
    }
}
