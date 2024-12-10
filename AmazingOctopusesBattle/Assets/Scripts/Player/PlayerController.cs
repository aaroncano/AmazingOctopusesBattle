using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float jumpForce = 40f;
    [SerializeField] private string moveInput = "Horizontal";
    [SerializeField] private string jumpInput = "Jump";
    [SerializeField] private string downInput = "Down1";
    [SerializeField] private ParticleSystem jumpParticles = null;

    [SerializeField] private int extraJumpNum = 0;
    private int extraJumpCount = 0;

    private float xMove;
    private bool facingRight = true;
    [SerializeField] private bool flipStart = false;
    private Rigidbody2D rb;

    //grounded
    private bool isGrounded;
    [SerializeField] private LayerMask ground = default;
    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private float checkGroundkRadius = 0f;
    [SerializeField] private float checkFrontRadius = 0f;

    //sliding
    private bool isFronted;
    [SerializeField] private Transform frontCheck = null;
    private bool wallSliding;
    [SerializeField] private float wallSlidingSpeed = 0f;

    [SerializeField] private float paralysisTime = 0f;
    private bool isParalyzed = false;

    [SerializeField] private float startStopTime = 0f;

    private void Awake()
    {
        if (flipStart == true) rotateStart();
        startStop();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumpCount = extraJumpNum;
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkGroundkRadius, ground);
        isFronted = Physics2D.OverlapCircle(frontCheck.position, checkFrontRadius, ground);

        checkFlip();
        xMove = Input.GetAxis(moveInput);

        if (isGrounded || isFronted) extraJumpCount = extraJumpNum;

        if (Input.GetButtonDown(jumpInput))
        {
            if (isGrounded)
            {
                FindObjectOfType<AudioManager>().Play("jump");
                rb.velocity = Vector2.up * jumpForce;
                jumpParticles.time = 0f;
                jumpParticles.Play();
            }
            else if (!isFronted && extraJumpCount > 0)
            {
                FindObjectOfType<AudioManager>().Play("jump");
                extraJumpCount--;
                rb.velocity = Vector2.up * jumpForce;
                jumpParticles.time = 0f;
                jumpParticles.Play();
            }
        }

        if (isFronted == true && isGrounded == false && xMove != 0) wallSliding = true;
        else wallSliding = false;

        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            if (Input.GetButton(jumpInput))
            {
                rb.velocity = Vector2.up * (jumpForce/2.4f);
            }
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkGroundkRadius);
        Gizmos.DrawWireSphere(frontCheck.position, checkFrontRadius);
    }

    private void FixedUpdate()
    {
        move();
    } 
    private void move()
    {
        if (isGrounded == true)
        {
            rb.velocity = new Vector2(xMove * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(xMove * speed, rb.velocity.y);
        }
    }

    private void checkFlip()
    {
        if(isParalyzed == false)
        {
            if ((facingRight == false && xMove > 0) || (facingRight == true && xMove < 0))
            {
                facingRight = !facingRight;
                transform.Rotate(0f, 180f, 0f);
            }
        }
    }

    private void rotateStart()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void getStuned()
    {
        speed = 0f;
        jumpForce = 0f;
        transform.Rotate(0f, 0f, 90f);
        gameObject.GetComponent<PlayerShooting>().disarmWeapon();
        isParalyzed = true;
    }

    public void getParalyzed()
    {
        if(isParalyzed == false)
        {
            float Rspeed = speed, RjumpForce = jumpForce;
            getStuned();
            StartCoroutine(wait(Rspeed, RjumpForce));
        }
    }

    IEnumerator wait(float Rspeed, float RjumpForce)
    {
        yield return new WaitForSeconds(paralysisTime);
        speed = Rspeed;
        jumpForce = RjumpForce;
        transform.Rotate(0f, 0f, -90f);
        gameObject.GetComponent<PlayerShooting>().armOldWeapon();
        isParalyzed = false;
    }

    public void startStop()
    {
        float Rspeed = speed, RjumpForce = jumpForce;
        speed = 0f;
        jumpForce = 0f;
        StartCoroutine(waitStartStop(Rspeed, RjumpForce));
    }
    IEnumerator waitStartStop(float Rspeed, float RjumpForce)
    {
        yield return new WaitForSeconds(startStopTime);
        speed = Rspeed;
        jumpForce = RjumpForce;
    }

    public float getXMove() { return xMove; }
    public bool getIsGrounded() { return isGrounded; }
    public bool getIsFronted() { return isFronted; }
    public bool getWallsliding() { return wallSliding; }
    public bool getIsParalyzed() { return isParalyzed; }
    public string getDownInput() { return downInput; }
}
