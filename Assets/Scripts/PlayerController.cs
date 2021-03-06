using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //The States
    public enum PlayerStates
    {
        IDLE,
        RUN,
        INAIR,
        STUNGRENADE,
        ROCKETLAUNCHER,
        RAILGUN,
        AIRSTRIKE
    }

    public PlayerStates pStates;

    //Movement
    public float moveSpeed;
    public float jumpForce;
    public float moveHorizontal;
    public bool moveJump;
    
    //Collision
    Rigidbody2D playerRB;
    public LayerMask whatIsGround;
    public float lengthToDetectGround = 0.5f;
    public float boxLength = 2f;
    Vector2 boxSize;
    public bool isGrounded;

    //Aiming
    public float aimHorizontal;
    public float aimVertical;
    float aimAngle;
    public Transform pivot;

    //IENumerator Shooting
    public Transform fireTransform;
    public GameObject bulletPrefab;
    public float fireDelay = 0.1f;
    public bool isShooting;
    public bool standShootInput;
    
    public float counter = 3;
    public float nextFire = 0f;
    public float fireRate = 0.5f;

    //Animation
    Animator playerAnim;

    bool soundCheckRun;

    private void Awake()
    {
        pStates = PlayerStates.IDLE;
        playerAnim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        boxSize = new Vector2(boxLength, lengthToDetectGround);
    }

    // Update is called once per frame
    void Update()
    {
        CheckGroundusingOverlapBox();

        if (GameManager.instance.gStates == GameManager.GameStates.GAMEPLAY)
        {
            moveHorizontal = Input.GetAxisRaw("LeftAnalogX");
            moveJump = Input.GetButtonDown("Jump");
            standShootInput = Input.GetButton("Fire1");
            aimHorizontal = Input.GetAxisRaw("RightAnalogX");
            aimVertical = Input.GetAxisRaw("RightAnalogY");
        }
        else if (GameManager.instance.gStates != GameManager.GameStates.GAMEPLAY)
        {
            moveHorizontal = 0f;
            moveJump = false;
            standShootInput = false;
            aimHorizontal = 0f;
            aimVertical = 0f;
        }

        playerAnim.SetFloat("HorizontalVelocity", moveSpeed * Mathf.Abs(moveHorizontal));
        playerAnim.SetBool("isGrounded", isGrounded);

       
        

        switch (pStates)
        {
            case PlayerStates.IDLE:
                soundCheckRun = false;
                if (AudioManager.instance.IsPlaying("FootstepSFX"))
                {
                    AudioManager.instance.StopSound("FootstepSFX");
                }
                //if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle"))
                //{
                //    playerAnim.Play("Player_Idle");
                //}
                if (moveHorizontal != 0)
                {
                    pStates = PlayerStates.RUN;
                }

                break;

            case PlayerStates.RUN:
                Move();

                if (!AudioManager.instance.IsPlaying("FootstepSFX"))
                {
                    AudioManager.instance.PlaySound("FootstepSFX");
                }
                
                //if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Player_Run"))
                //{
                //    playerAnim.Play("Player_Run");
                //}
                if (moveHorizontal == 0)
                {
                    pStates = PlayerStates.IDLE;
                }

                break;

            case PlayerStates.INAIR:
                Move();
                break;

            case PlayerStates.STUNGRENADE:
                break;

            case PlayerStates.ROCKETLAUNCHER:
                break;

            case PlayerStates.RAILGUN:
                break;

            case PlayerStates.AIRSTRIKE:
                break;
        }

        


        if (!isMoving() && standShootInput && isGrounded && Time.time > nextFire)
        {
            StandShootBoolean();
        }

        if (moveJump && isGrounded)
        {
            Jump();
        }

        if (aimHorizontal > 0.1f || aimHorizontal < -0.1f || aimVertical > 0.1f || aimVertical < -0.1f)
        {
            ProjectileDirection();
        }
            
    }

    void CheckGroundusingOverlapBox()
    {
        Vector2 boxCenter = (Vector2)transform.position + (Vector2.down * boxSize.y * 0.5f);

        isGrounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, whatIsGround) != null);

        if (isGrounded)
        {
            if (moveHorizontal != 0)
            {
                pStates = PlayerStates.RUN;
            }
            else if(moveHorizontal == 0)
            {
                pStates = PlayerStates.IDLE;
            }
        }
        else if(!isGrounded)
        {
            pStates = PlayerStates.INAIR;
        }


    }

    private void Move()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            playerRB.velocity = new Vector2(moveSpeed * moveHorizontal, playerRB.velocity.y);
            
        }
      
    }

    void Jump()
    {
        playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }


    //IEnumerator StandShoot()
    //{
       
    //    while (standShootInput && !isMoving())
    //    {

    //        nextFire = Time.time + fireDelay;
    //        Instantiate(bulletPrefab, fireTransform.position, Quaternion.identity);
    //        Debug.Log(Time.time);
    //        yield return new WaitForSeconds(fireDelay);
    //    }
        
    //}

    void StandShootBoolean()
    {
        nextFire = Time.time + fireRate;
        Instantiate(bulletPrefab, fireTransform.position, fireTransform.rotation);
    }


    bool isMoving()
    {
        if (playerRB.velocity == Vector2.zero)
        {
            return false;
        }
        else if (playerRB.velocity != Vector2.zero)
        {
            return true;
        }

        return false;
    }

    void ProjectileDirection()
    {
        aimAngle = Mathf.Rad2Deg * Mathf.Atan2(aimVertical, aimHorizontal);
        pivot.rotation = Quaternion.Euler(0f,0f, aimAngle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelTrigger"))
        {
            GameManager.instance.gStates = GameManager.GameStates.LEVELOUTRO;
        }
    }

    
}
