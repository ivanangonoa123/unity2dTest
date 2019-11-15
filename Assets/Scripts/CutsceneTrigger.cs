using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector director;
    public PlayerMovement playerMovement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement.freeze = true;
            director.stopped += OnPlayableDirectorStopped;
            director.Play();
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            Debug.Log("PlayableDirector stopped");
            director.stopped -= OnPlayableDirectorStopped;
            playerMovement.freeze = false;
            Destroy(gameObject);
        }
    }
}
