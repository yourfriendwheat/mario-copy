using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Persist : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        int numScenePersists = 0;
        Scene currentScene = SceneManager.GetActiveScene();
        GameObject[] allObjects = currentScene.GetRootGameObjects();

        foreach (var obj in allObjects)
        {
            Persist persistComponent = obj.GetComponent<Persist>();
            if (persistComponent != null)
            {
                numScenePersists++;
            }
        }

        if (numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
