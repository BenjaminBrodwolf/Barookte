
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestPlayLevel : MonoBehaviour
{
    // Type in the name of the Scene you would like to load in the Inspector
    public string testPlaySceneName;

    // Assign your GameObject you want to move Scene in the Inspector
    public GameObject buildetLevelGameObject;
    void Update()
    {
        // Press the space key to add the Scene additively and move the GameObject to that Scene
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Start TestPlayLevel");
  
            StartCoroutine(LoadTestPlayLevelAsync());
        }
    }

    IEnumerator LoadTestPlayLevelAsync()
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(testPlaySceneName, LoadSceneMode.Single);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
        SceneManager.MoveGameObjectToScene(buildetLevelGameObject, SceneManager.GetSceneByName(testPlaySceneName));
        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
