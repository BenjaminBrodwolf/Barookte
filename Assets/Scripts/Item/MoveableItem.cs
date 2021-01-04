using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveableItem : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private bool playerTrigger;
    private Vector3 turnPosition;
    private Vector3 previousTurnPosition;

    //For Animation
    public float animationTime = 0.2f;
    private float timeElapsed;

    public bool IsPlayerInTriggerToItem() => playerTrigger;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        turnPosition = transform.position;
        timeElapsed = animationTime;

        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name != "LevelBuilder")
        {
            rigidbody.isKinematic = false; //myPrefab.GetComponent<Rigidbody>().isKinematic;
        }
    }


    private void Update()
    {
        if (timeElapsed < animationTime)
        {
             UpdatePositionEveryFrame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Trigger with Player !");
            playerTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTrigger = false;
        }
    }

    public void PushItemInDirection(Vector3 newDirection)
    {
        if (!Physics.Raycast(turnPosition, newDirection, 1f))
        {
            timeElapsed = 0;
            previousTurnPosition = turnPosition;
            turnPosition = turnPosition + newDirection;
            audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            audioSource.Play();
        }
    }

    private void UpdatePositionEveryFrame(double animationAccuracy = 0.05)
    {
        var pos = Vector3.Lerp(previousTurnPosition, turnPosition, timeElapsed / animationTime);
        timeElapsed += Time.deltaTime;
        rigidbody.MovePosition(pos);
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        if (timeElapsed >= animationTime)
        {
            rigidbody.MovePosition(turnPosition);
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
        
    }
}