using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCamera : MonoBehaviour
{
    private static PlayerCamera _control;

    private void Awake()
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
    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "LevelBuilder")
        {
            Debug.Log("Deactive Player Camera");
            gameObject.SetActive(false); 
        }
    }
}
