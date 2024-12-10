using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerController controller;
    private Rigidbody2D rb;
    private PlayerHealth ph;

    private const string idle = "Player_Idle";
    private const string moving = "Player_Moving";
    private const string jumping = "Player_JumpingUp";
    private const string falling = "Player_JumpingDown";
    private const string wallUp = "Player_WallslidingUp";
    private const string wallDown = "Player_WallslidingDown";
    private const string paralyzed = "Player_Paralyzed";
    private const string dead = "Player_Dead";

    private string currentAnim;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        ph = GetComponent<PlayerHealth>();
    }
    private void changeAnim(string newAnim)
    {
        if (currentAnim == newAnim) return;
        animator.Play(newAnim);
        currentAnim = newAnim;
    }
    private void Update()
    {
        if(ph.getHealth() > 0)
        {
            if(controller.getIsParalyzed() == false)
            {
                if (controller.getXMove() != 0)
                {
                    if (controller.getIsGrounded() == true) changeAnim(moving);
                    else
                    {
                        if(rb.velocity.y >= 0)
                        {
                            if (controller.getWallsliding() == true) changeAnim(wallUp);
                            else changeAnim(jumping);
                        }
                        else
                        {
                            if (controller.getWallsliding() == true) changeAnim(wallDown);
                            else changeAnim(falling);
                        }
                    }
                }
                else
                {
                    if (controller.getIsGrounded() == true) changeAnim(idle);
                    else
                    {
                        if (rb.velocity.y > 0) changeAnim(jumping);
                        else changeAnim(falling);
                    }
                }

            }
            else
            {
                changeAnim(paralyzed);
            }
        }
        else
        {
            changeAnim(dead);
        }
    }

}
