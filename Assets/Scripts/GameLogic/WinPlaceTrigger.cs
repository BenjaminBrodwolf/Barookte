﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPlaceTrigger : MonoBehaviour
{
    private GameManager gameManagerScript;
    private Scene currentScene;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    void Start()
    {
        Debug.Log(currentScene.name);
        if (currentScene.name != "LevelBuilder")
        {
            gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
    }

    // Player on bridge -> WIN
    private void OnTriggerEnter(Collider other)
    {
        if (currentScene.name != "LevelBuilder" && other.CompareTag("Player"))
        {
            Debug.Log(currentScene.name);

            Debug.Log("Trigger with Player Win !");
            gameManagerScript.StartBlackout();
            gameManagerScript.WaitForSecondsFunctionAndRestart(3);
        }
    }
}