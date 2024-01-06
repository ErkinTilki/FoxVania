using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static soundManager instance;
    public AudioSource coinsSource;
    public AudioClip coinSound;
    
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        coinsSource = GetComponent<AudioSource>();
        
    }

    
    void Update()
    {
        
    }
}
