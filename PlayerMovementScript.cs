using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    bool alive = true;
    [Header("Speed / Control")]
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpHeight = 16f;
    [SerializeField] float climbVelo = 4f;
    [SerializeField] float dashSpeed = 30f;
    [SerializeField] Vector2 deathKick = new Vector2(0f, 20.35f);
    [SerializeField] Vector2 dashKick = new Vector2(20f, 0f);

    [Header("Cheats / Features")]
    [SerializeField] bool DoubleJump = false;
    [SerializeField] bool infiniteAmmo = false; 
    
    public float StartDashTimer;
    private float CurrentDashTime;
    private float dashCD = 0f; 
    bool isDashing;
    bool isGrounded = false;
    float dashDirection;
    bool playerHasHorizonalSpeed;
    bool canDash { get; set; }
    public bool isInvincible {get;set;} = false;
    float invincibleTimer = 13f;

    [Header("GameObjekte")]
    [SerializeField] AudioClip clip;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] GameObject music;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem particles;
    
    Vector2 moveInput;
    Animator animator;
    GameSession session;
    Vector2 jumpInput;
    Rigidbody2D playerRigid;
    LayerMask layer;
    CapsuleCollider2D bodyCollider2D;
    BoxCollider2D feetBox;




    void Start()
    {
        //Spieler Körper holen
        playerRigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        
        bodyCollider2D = GetComponent<CapsuleCollider2D>();
        feetBox = GetComponent<BoxCollider2D>();
        
        deathParticles.Stop();
        particles.Stop();
        
    }

    
    void Update()
    {
        
        if(!alive){return;}

        isGrounded = feetBox.IsTouchingLayers(LayerMask.GetMask("Ground"));
        run();
        //Debug.Log(dashCD);
        
        if(dashCD <= 0)
        {
            this.canDash = true;

        }
        else
        {
            dashCD -= Time.deltaTime;
            
            
        }

        if (invincibleTimer <= 0)
        {
            music.SetActive(true);
            isInvincible = false;
            
        }
        else if(isInvincible)
        {
            music.SetActive(false);
            invincibleTimer -= Time.deltaTime;

        }else
        {
            isInvincible = false;
        }
 
        
        flipsprite();
        ClimbLadder();
        die();
       



    }


    public void setInvicible()
    {
        isInvincible = true;
    }

    public void resetInvinc()
    {
        
        isInvincible = false;
    }

    

    private void flipsprite()
    {
        //Feststellen ob Spieler Geschwindigkeit größer als 0 ist 
        playerHasHorizonalSpeed = Mathf.Abs(playerRigid.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizonalSpeed)
        {
            //Spielermodel umdrehen -> Animation umkehren
            transform.localScale = new Vector2(Mathf.Sign(playerRigid.velocity.x), 1f);
        }
        
    }

    public float returnInvicTimer()
    {
        return invincibleTimer;
    }


    private void run()
    {
        // Spieler InputSpeed hinzufügen -> immer Vektoren
        Vector2 playerVelo = new Vector2(moveInput.x*speed, playerRigid.velocity.y);
        playerRigid.velocity = playerVelo;
        if(Mathf.Abs(playerRigid.velocity.x) > Mathf.Epsilon)
        {
            animator.SetBool("isRunning", true);
        }else{ animator.SetBool("isRunning", false);}
    }

    void ClimbLadder()
    {
        if(bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            

            Vector2 climberino = new Vector2(playerRigid.velocity.x, moveInput.y*climbVelo);
            playerRigid.velocity = climberino;

            if(Mathf.Abs(playerRigid.velocity.y)> Mathf.Epsilon){
                animator.SetBool("isClimbing", true);
                playerRigid.gravityScale = 0;

            }//else{animator.SetBool("isClimbing", false);}
        }else{animator.SetBool("isClimbing",false); playerRigid.gravityScale = 4;}
        


    }
    void OnMove(InputValue value)
    {
        if(!alive){return;}
        animator.SetBool("isShooting", false);
        // Vom Controller 2D Vektor holen x,y 
        moveInput = value.Get<Vector2>();

        
        

    }
    void OnJump(InputValue value)
    {
        if(!alive){return;}
        animator.SetBool("isShooting", false);
        if(!DoubleJump)
        {
            if(!feetBox.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}
        }else
        {
            if(!bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}
        }
        if(value.isPressed)
        {

            playerRigid.velocity += new Vector2(0f, jumpHeight);
        }

    }

    void OnFire(InputValue value)
    {
        
        if(!alive || FindObjectOfType<PauseMenu_>().getStatus()){return;}
        if(this.animator.GetCurrentAnimatorStateInfo(0).IsName("running")){return;}
        if(!infiniteAmmo)
        {
            if(FindObjectOfType<GameSession>().ammo == 0)
            {
                return;
            } 
            FindObjectOfType<GameSession>().shootShot();
        }
        animator.SetBool("isShooting", true);
        Instantiate(bullet, gun.position, transform.rotation);
        
    }

    void OnInteract(InputValue value)
    {
        if(bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("interactables")))
        {
            FindObjectOfType<LevelExit>().goNext();
        }else if(bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("levers")))
        {
            Debug.Log("leverFound");
            FindObjectOfType<leverScript>().pushLever();
        }
    }

    void OnDash(InputValue value)
    {
        if (this.canDash) { Dasherino(); }

    }

    private void Dasherino()
    {
        if (!isGrounded)
        {


            if (!alive || FindObjectOfType<PauseMenu_>().getStatus()) { return; }
            isDashing = true;
            CurrentDashTime = StartDashTimer;
            dashDirection = playerRigid.velocity.x;
;
            if (isDashing)
            {


                playerRigid.AddForce(new Vector2(dashDirection * dashKick.x, 0f));
                particles.Play();
                this.canDash = false;
                dashCD = 20f;
                CurrentDashTime -= Time.deltaTime;
                if (CurrentDashTime <= 0)
                {
                    isDashing = false;
                    
                    
                }
            }
        }
    }

 

    private void die()
    {
        if (isInvincible) { return; }
        if(bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemies")) || bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("deathZone")))
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            alive = false;
            animator.SetTrigger("Dying");
            playerRigid.velocity += deathKick;
            deathParticles.Play(); 
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            
            

        }


    }
}
