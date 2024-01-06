using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    PolygonCollider2D myBodyCollider;
    BoxCollider2D myBoxCollider;
    bool isAlive = true;


    void Start()
    {
        if(!isAlive){return;}
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myBodyCollider = GetComponent<PolygonCollider2D>();
        
    }

    
    void Update()
    {
        if(!isAlive){return;}
        Die();
        
    }


    void Die()
     {
        if(myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {   
            
            Invoke("enemyDeath",0.1f);
            isAlive = false;
            myAnimator.SetTrigger("isHurt");
        
        }
     }


void enemyDeath()
    {
            Destroy(myRigidBody);
            Destroy(myBoxCollider);
            Destroy(myBodyCollider);
   }
}
    

