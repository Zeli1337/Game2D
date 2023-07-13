using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invinciPickUp : MonoBehaviour
{

    [SerializeField] AudioClip audioClip;
    [SerializeField] AudioClip powerUpSound;
    bool collected = false;
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag.Equals("Player")&& !collected)
        {
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
            FindObjectOfType<PlayerMovementScript>().setInvicible();
            AudioSource.PlayClipAtPoint(powerUpSound, Camera.main.transform.position);
            collected = true;
            Destroy(gameObject);
        }    
    }
}
