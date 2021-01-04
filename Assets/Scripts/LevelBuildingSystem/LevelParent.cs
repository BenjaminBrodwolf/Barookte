using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelParent : MonoBehaviour
{
    private static LevelParent _control;

    public string testPlaySceneName;
    public string buildySceneName;
    void Start()
    {        
        
        if (_control == null)
        {
            DontDestroyOnLoad(gameObject);
            _control = this;
        }
        else if (_control != this)
        {
            Destroy(gameObject);
        }
        
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "LevelBuilder")
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Start TestPlayLevel");
  
            SceneManager.LoadScene(testPlaySceneName, LoadSceneMode.Single);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Start BuildLevelScene");
  
            SceneManager.LoadScene(buildySceneName, LoadSceneMode.Single);
        }
    }
}
