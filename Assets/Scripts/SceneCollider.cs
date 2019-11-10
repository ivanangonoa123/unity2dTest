using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneCollyderType {
    Scene,
    Spot
};

public class SceneCollider : MonoBehaviour
{
    public string sceneName;
    public string nextSpawnPosition;
    public SceneCollyderType type;
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
        if (collision.CompareTag("Player"))
        {
            if (type == SceneCollyderType.Scene)
            {
                LoadScene(sceneName);
            }
            else
            {
                GameObject spot = GameObject.FindGameObjectWithTag(sceneName);
                Vector3 spotPos = spot.transform.position;
                Camera.main.transform.position = new Vector3(spotPos.x, spotPos.y, Camera.main.transform.position.z);
            }
        }
    }

    // @TODO move to scene manager
    void LoadScene(string sceneName)
    {
        GameManager.Instance.SpawnPosition = nextSpawnPosition;
        SceneManager.LoadScene(sceneName);
    }
}
