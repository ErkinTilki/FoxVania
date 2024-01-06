using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] public int playerLives = 3;
    [SerializeField] float playerDeathTime = 1.5f;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int Score = 0;
    
    void Awake()
    {
        int numOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if( numOfGameSessions > 1)
        {
            Destroy(gameObject);

        }
        else 
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    

    public void ProcessPlayerDeath()
     {
        if (playerLives > 1)
        {
            Invoke("TakeLife",playerDeathTime);
        }
        else
        {
            Invoke("ResetGameSession",playerDeathTime);
        }
     }

    private void ResetGameSession()
    {
        FindAnyObjectByType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        playerLives--;
    } 

    public void AddToScore(int pointsToAdd)
    {
        Score += pointsToAdd;
        scoreText.text = Score.ToString(); 
    }
}
