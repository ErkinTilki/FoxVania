using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
   Rigidbody2D myRigidBody;
   Vector2 moveInput;
   Animator myAnimator;
   BoxCollider2D myBoxCollider;
   CapsuleCollider2D myBodyCollider;

   float gravityAtStart; 
   bool isAlive = true;
   
   
   [SerializeField] float runSpeed = 8f;
   [SerializeField] float jumpSpeed = 14f;
   [SerializeField] float climbSpeed = 5f;
   [SerializeField] float deathKick = 15f;
   [SerializeField] float killKick = 15f;
   
    void Start()
    {
       myRigidBody = GetComponent<Rigidbody2D>();
       myAnimator = GetComponent<Animator>();
       myBoxCollider = GetComponent<BoxCollider2D>();
       myBodyCollider = GetComponent<CapsuleCollider2D>();
       gravityAtStart = myRigidBody.gravityScale;
       
    }

    void Update()
    {
        if(!isAlive){return;}
        Run();
        flipPlayer();
        ClimbLadder();
        Die();
    }

   void OnMove(InputValue value)
   {
        if(!isAlive){return;}
        moveInput = value.Get<Vector2>();
   }

    void OnJump(InputValue value)
    {
        if(!isAlive){return;}
        if(!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}

        if(value.isPressed)
        {
            myRigidBody.velocity += new Vector2 (0f, jumpSpeed);
        }
        
    }

   void Run()
   {
       Vector2 playerMovement = new Vector2(moveInput.x* runSpeed, myRigidBody.velocity.y); 
       myRigidBody.velocity = playerMovement;

       bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

       myAnimator.SetBool("isRunning",playerHasHorizontalSpeed);

      
   }

   void flipPlayer()
   {
    bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

    if(playerHasHorizontalSpeed)
    {
    transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f);
    }
   }

   void ClimbLadder()
   {
        if(!myRigidBody.IsTouchingLayers(LayerMask.GetMask("Stairs")))
        {myRigidBody.gravityScale = gravityAtStart;
        myAnimator.SetBool("isClimbing",false);
         return;
        }
         
        Vector2 climbingMovement = new Vector2(myRigidBody.velocity.x, moveInput.y* climbSpeed);
        myRigidBody.velocity = climbingMovement;
        myRigidBody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing",playerHasVerticalSpeed);
   }

   void Die()
   {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
           
            {
            isAlive = false;
            myAnimator.SetTrigger("isHurt");
            myRigidBody.velocity = new Vector2 (0f,deathKick);
            Destroy(myBodyCollider);
            Destroy(myBoxCollider);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            }
        }else if(myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            isAlive = true;
            myRigidBody.velocity = new Vector2 (0f,killKick);

        }
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards"))){
            isAlive = false;
            myAnimator.SetTrigger("isHurt");
            myRigidBody.velocity = new Vector2 (0f,deathKick);
            Destroy(myBodyCollider);
            Destroy(myBoxCollider);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
   }
}
