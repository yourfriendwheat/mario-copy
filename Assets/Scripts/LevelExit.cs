using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        Debug.Log("LoadNextLevel started.");
        
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

      
        Object.FindFirstObjectByType<Persist>()?.ResetScenePersist();

        
        try
        {
            Debug.Log("Attempting to load scene at index: " + nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load scene: " + e.Message);
        }
    }
}
