using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    Animator myAnimator; 
    [SerializeField] int pointsAdded = 1;
    bool wasCollected = false;

    void Start()
    
    {
        myAnimator = GetComponent<Animator>();
        
        
    }
    private void OnTriggerEnter2D(Collider2D other) 

    {
        if(other.tag == "Player" && !wasCollected)
        {   
            wasCollected = true;
            FindAnyObjectByType<GameSession>().AddToScore(pointsAdded);
            myAnimator.SetTrigger("isTaken");

        }    
        
    }
    
}
