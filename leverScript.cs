using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverScript : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject door;
    [SerializeField] AudioClip doorOpen;
    [SerializeField] AudioClip doorClose;


    //Only use in combination w/ door! hook up door in Serialized field
    private void Start() 
    {
        animator = GetComponent<Animator>();
        animator.SetBool("leverStatus", false);
        
        
    }

    public void pushLever()
    {
        if (animator.GetBool("leverStatus") == false)
        {
            animator.SetBool("leverStatus", true);
            door.GetComponent<SpriteRenderer>().enabled = false;
            door.GetComponent<BoxCollider2D>().enabled = false;
            AudioSource.PlayClipAtPoint(doorOpen, Camera.main.transform.position);
        }
        else
        {
            animator.SetBool("leverStatus", false);
            door.GetComponent<SpriteRenderer>().enabled = true;
            door.GetComponent<BoxCollider2D>().enabled = true;
            AudioSource.PlayClipAtPoint(doorClose, Camera.main.transform.position);

        }
    }

}
