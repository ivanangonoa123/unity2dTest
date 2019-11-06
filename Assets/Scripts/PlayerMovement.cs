using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    bool canJump;
    // Start is called before the first frame update
    void Start()
    {
        SetSpawnPosition();
    }

    void Awake()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;

        if (movement.x != 0)
        {
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = movement.x <  0;
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("moving", false);
        }

        if (Input.GetButtonDown("Jump") && canJump)
        {
            canJump = false;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
        }
    }

    private void SetSpawnPosition()
    {
        string nextSpawnPosition = GameManager.Instance.SpawnPosition;
        if (nextSpawnPosition != null)
        {
            GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint" + nextSpawnPosition);

            if (spawnPoint)
            {
                // si tenemos una spawnPosition movemos el player ahi
                transform.position = spawnPoint.transform.position;
                // lo flipeamos si es right o left (fijate la parte derecha de la asignacion,
                // que va a dar true o false si es LEFT/RIGHT)
                gameObject.GetComponent<SpriteRenderer>().flipX = nextSpawnPosition == "RIGHT";
            }
            else
            {
                // lo seteamos a la izquierda nomas
                transform.position = new Vector3(-7f, -2f, 0);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            canJump = true;
        }
    }
}
