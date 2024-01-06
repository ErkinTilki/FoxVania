using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
        [SerializeField] float waitTime = 0.5f;
    
         void OnTriggerEnter2D(Collider2D other) {
            StartCoroutine(levelDelay());
         }

         IEnumerator levelDelay()
         {
            yield return new WaitForSecondsRealtime(waitTime);
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                int nextSceneIndex = currentSceneIndex + 1;
                
                if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
                {
                  nextSceneIndex = 0;
                }
               FindAnyObjectByType<ScenePersist>().ResetScenePersist();
                SceneManager.LoadScene(currentSceneIndex + 1);
         }
}

