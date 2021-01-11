using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TriggerBox : MonoBehaviour
{
    private Scene currentScene;
    private GameObject player;

    public GameObject[] enemys;

    private void Awake()
    {
    }

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentScene.name != "LevelBuilder" && other.CompareTag("Player"))
        {
     
        }
    }
}
