using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlaceTrigger : MonoBehaviour
{
    private GameManager gameManagerScript;
    void Start()
    {
        gameManagerScript =  GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    // Player on bridge -> WIN
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             Debug.Log("Trigger with Player Win !");
            gameManagerScript.StartBlackout();
            gameManagerScript.WaitForSecondsFunctionAndRestart(3);
        }
    }
}
