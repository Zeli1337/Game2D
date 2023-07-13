using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartPick : MonoBehaviour
{
    CircleCollider2D circleCollider2D;

    [Header("AudioClip Herz")]
    [SerializeField] AudioClip clip;

    bool collected = false;



    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag.Equals("Player") && !collected)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            FindObjectOfType<GameSession>().heartCollected();
            Destroy(gameObject);
            collected = true;
        }

    }

}
