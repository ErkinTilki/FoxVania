using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newMovementScript : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    PolygonCollider2D myBodyCollider;
    BoxCollider2D myBoxCollider;

    [SerializeField] float moveSpeed = 4f;
    public GameObject  pointA;
    public GameObject  pointB;
    private Transform currentPoint;
    bool isAlive = true;
    
    void Start()
    {
        if(!isAlive){return;}
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myBodyCollider = GetComponent<PolygonCollider2D>();
        currentPoint = pointB.transform;
    }

    
    void Update()
    {if(!isAlive){return;}
        
        Die();
        
        if(!isAlive){return;}
        Run();
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            myRigidBody.velocity = new Vector2(moveSpeed,0f);

        }else{
             myRigidBody.velocity = new Vector2(-moveSpeed,0f);
        }
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
        }
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
        }
        
        
    }
    
    private void flip()
    {if(!isAlive){return;}
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    void Run()
     {if(!isAlive){return;}
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        
     }

     void Die()
     {
        if(myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {   
            
            Invoke("enemyDeath",0.1f);
            isAlive = false;
            myRigidBody.velocity = new Vector2 (0f,0f);
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

